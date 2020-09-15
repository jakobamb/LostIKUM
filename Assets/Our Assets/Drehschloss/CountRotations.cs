using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CountRotations : MonoBehaviour
{
    public UnityEvent onFullRotation;
    public UnityEvent onUnlock;

    public List<int> CORRECT_COMBI = new List<int>() { 1, -1, 1 };
    public List<int> _currentCombination = new List<int>();

    public SchrankTransitionMgr schrank;

    private float _cumulatedRotation = 0.0f; 
    private float _lastFrameEulerRotation = 0f;

    //Wird in jedem Frame aufgerufen
    void Update ()
    {
        UpdateCumulatedRotation();

        if (Mathf.Abs(_cumulatedRotation) > 360) {
            triggerFullRotation();
        }
    }

    //updates _cumulatedRotation and _lastFrameEulerRotation
    private void UpdateCumulatedRotation(){

        float FrameEulerRotation = transform.localEulerAngles.z;
        float relativeFrameRotation;

        if (FrameEulerRotation - _lastFrameEulerRotation < -180) {
            //Debug.Log("Rechts Über die Null");
            relativeFrameRotation = 360 - _lastFrameEulerRotation + FrameEulerRotation;
        } else if (FrameEulerRotation - _lastFrameEulerRotation  >= 180) {
            //Debug.Log("Links Über die Null");
            relativeFrameRotation = 360 - FrameEulerRotation + _lastFrameEulerRotation;
        }
        else {
            relativeFrameRotation = FrameEulerRotation - _lastFrameEulerRotation;
        }

        _cumulatedRotation += relativeFrameRotation;
        _lastFrameEulerRotation = FrameEulerRotation;
    }



    //adds item to list after 360 left / right rotation
    private void triggerFullRotation()
    {
        if (_cumulatedRotation > 360)
        {
            //add +1 to list -> complete right turn
            _currentCombination.Add(+1);
            Debug.Log("[CountRotations] 360 Grad nach rechts gedreht");
        }
        else if (_cumulatedRotation < -360)
        {
            //add -1 to list -> complete left turn
            _currentCombination.Add(-1);
            Debug.Log("[CountRotations] 360 Grad nach links gedreht");
        }
        // reset the button
        _cumulatedRotation = 0;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        _lastFrameEulerRotation = 0;

        // check if the combination is correct
        if (isCombiCorrect())
        {
            correctCombiHandler();
            return;
        }

        // StartCoroutine(disableRotation());
        onFullRotation.Invoke();
    }

    //checks if the current combination is correct
    private bool isCombiCorrect() {
        
        for (int i = 0; i < _currentCombination.Count; i++) {
            if (_currentCombination[i] != CORRECT_COMBI[i])
            {
                _currentCombination.Clear();
                return false;
            }
        }

        if (_currentCombination.Count == CORRECT_COMBI.Count)
        {
            // Both lists have been successfully traversed.
            return true;
        }

        return false;
    }

    //handles the correct combination
    private void correctCombiHandler(){

        // tell the Transition Manager
        schrank.DrehschlossUnlocked();
        Debug.Log("Correct Combination!");
        onUnlock.Invoke();
    }
}
