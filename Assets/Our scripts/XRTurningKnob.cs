using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XRTurningKnob : XRBaseInteractable
{
    private XRBaseInteractor _grabInteractor = null;
    private bool _isGrabbed = false;
    private float _previousZRotation = 0;

    protected override void Awake()
    {
        base.Awake();
        onSelectEnter.AddListener(StartSelect);
        onSelectExit.AddListener(EndSelect);
    }

    private void Update()
    {
        if (_isGrabbed && _grabInteractor != null)
        {
            // calculate how much the XR controller has been turned
            float currentZRotation = _grabInteractor.transform.eulerAngles.z;
            float relativeRotationDifference;

            if (currentZRotation - _previousZRotation < -180)
            {
                relativeRotationDifference = 360 - _previousZRotation + currentZRotation;
            }
            else if (currentZRotation - _previousZRotation >= 180)
            {
                relativeRotationDifference = 360 - currentZRotation + _previousZRotation;
            }
            else
            {
                relativeRotationDifference = currentZRotation - _previousZRotation;
            }

            // turn the knob
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - relativeRotationDifference);

            _previousZRotation = currentZRotation;
        }
    }

    private void OnDestroy()
    {
        onHoverEnter.RemoveListener(StartSelect);
        onHoverExit.RemoveListener(EndSelect);
    }

    private void StartSelect(XRBaseInteractor interactor)
    {
        _grabInteractor = interactor;
        _previousZRotation = interactor.transform.eulerAngles.z;
        _isGrabbed = true;
    }

    private void EndSelect(XRBaseInteractor interactor)
    {
        _grabInteractor = null;
        _isGrabbed = false;
    }
}
