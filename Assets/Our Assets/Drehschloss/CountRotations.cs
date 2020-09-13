using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRotations : MonoBehaviour
{   
    public List<int> CORRECT_COMBI = new List<int>() { 1, 1, -1, 1, -1, 1 };
    public AudioClip rotationSound;
    public AudioClip successSound;

    public SchrankTransitionMgr schrank;

    private CurrentCombination combination = new CurrentCombination();
    private bool clockWise; //true: im Uhrzeigersinn, false: gegen Uhrzeigersinn
    private float currentRotationValue;
   
    

    private bool _isSelected = false;
    private float _cumulatedRotation = 0.0f; 
    private float _lastFrameEulerRotation;
    


    //Wird beim Starten des Spiels einmalig initialisiert
    void Start()
    {
        _lastFrameEulerRotation = 90.0f;
        //GetComponent.hingeJoint..as.
        //rigidbody auf Sleep für 1 Sekunde

        schrank.LockNonMirrorSide();
         
    }



    //Wird beim Loslassen des Zylinders aufgerufen
    public void OnRelease()
    {
        _isSelected = false;
    }

    //Wird beim Anfassen des Zylinders aufgerufen
    public void OnSelect()
    {
        _isSelected = true;
    }

   

    //Wird in jedem Frame aufgerufen
    void Update ()
    {
       UpdateCumulatedRotation();
       trigger360Degree();
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
        //Debug.Log(_cumulatedRotation);
            
        
    }



    //adds item to list after 360 left / right rotation
    private void trigger360Degree(){

        if(_cumulatedRotation > 360){
            //add +1 to list -> complete right turn
            combination.addCombiValue(+1);
             //set _cumulatedRotation = 0 and  _lastFrameEulerRotation = 90.0f;
            _cumulatedRotation = 0;
            _lastFrameEulerRotation = 90.0f;
            Debug.Log("360 Grad nach rechts gedreht");

            //calls the disableRotation function
            StartCoroutine(disableRotation());
        } else if (_cumulatedRotation < -360){
            //add -1 to list ->complete left turn
            combination.addCombiValue(-1);
            //set _cumulatedRotation = 0 and  _lastFrameEulerRotation = 90.0f;
            _cumulatedRotation = 0;
            _lastFrameEulerRotation = 90.0f;
            Debug.Log("360 Grad nach links gedreht");

            //calls the disableRotation function
            StartCoroutine(disableRotation());
        }
    }


    //called by trigger360degree
    //freezes the button for a second
    //calls correctCombiHandler if Combi is correct
    //plays sound
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
            //play sound
            SFXPlayer.Instance.PlaySFX(rotationSound, transform.position , new SFXPlayer.PlayParameters()
            {
                Volume = 1.0f,
                Pitch = Random.Range(1.0f, 1.0f),
                SourceID = 1
            }, 0.5f, false);

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

        schrank.UnlockNonMirrorSide();

         //plays sound
         SFXPlayer.Instance.PlaySFX(successSound, transform.position , new SFXPlayer.PlayParameters()
        {
            Volume = 1.0f,
            Pitch = Random.Range(1.0f, 1.0f),
            SourceID = 1
        }, 0.5f, false);
        Debug.Log("Correct Combination!");
        schrank.UnlockNonMirrorSide();
        combination.clearCombi();
    }
}
