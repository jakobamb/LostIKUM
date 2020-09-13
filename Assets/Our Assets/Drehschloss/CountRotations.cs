using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CountRotations : MonoBehaviour
{
    public UnityEvent onFullRotation;
    public UnityEvent onUnlock;

    public List<int> CORRECT_COMBI = new List<int>() { 1, -1, 1};

    public SchrankTransitionMgr schrank;

    private CurrentCombination combination = new CurrentCombination();   

    private bool _isSelected = false;
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

        Debug.Log(_cumulatedRotation);
        // Debug.Log(_lastFrameEulerRotation);
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
            combination.addCombiValue(+1);
            Debug.Log("[CountRotations] 360 Grad nach rechts gedreht");
        }
        else if (_cumulatedRotation < -360)
        {
            //add -1 to list -> complete left turn
            combination.addCombiValue(-1);
            Debug.Log("[CountRotations] 360 Grad nach links gedreht");

        }
        // reset the button
        _cumulatedRotation = 0;
        transform.localEulerAngles = new Vector3(0, 0, 0);
        _lastFrameEulerRotation = 0;

        //_lastFrameEulerRotation = 0f;

        // StartCoroutine(disableRotation());
        onFullRotation.Invoke();
    }


    //called by trigger360degree
    //freezes the button for a second
    //calls correctCombiHandler if Combi is correct
    //plays sound
    // What is this for???
    IEnumerator disableRotation(){
        
        HingeJoint hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;

        hinge.useLimits = true;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;


         //checks if combi is correct
        if(isCombiCorrect()){
            correctCombiHandler();
            yield return new WaitForSeconds(1);
            hinge.useLimits = false;
        } else {
            yield return new WaitForSeconds(1);
            hinge.useLimits = false;
        }
    }


    //checks if the current combination is correct
    private bool isCombiCorrect() {
        List<int> currentCombination = combination.getCurrentCombination();
        bool isCombiCorrect = false;
 
        if(currentCombination.Count >= CORRECT_COMBI.Count) {
            //prüft paarweise, ob die Listen übereinstimmen
            for (int i = 0; i < CORRECT_COMBI.Count-1; i++){
                if(!(CORRECT_COMBI[i] == currentCombination[currentCombination.Count-CORRECT_COMBI.Count+i])) {
                    //gibt false zurück, falls Listen an einer Stelle nicht übereinstimmen
                    return false;
                }
            }
            
            //wird auf true gesetzt, nachdem die Schleife fertig gelaufen ist.
            isCombiCorrect=true;
        }
        return isCombiCorrect;
    }

    //handles the correct combination
    private void correctCombiHandler(){

        // tell the Transition Manager
        schrank.DrehschlossUnlocked();

        Debug.Log("Correct Combination!");
        combination.clearCombi();
    }
}
