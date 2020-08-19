using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRotations : MonoBehaviour
{   
    public List<int> CORRECT_COMBI = new List<int>() { 1, 1, -1, 1, 1, 1 };
    private bool _doorOpen = false;

    private bool clockWise; //true: im Uhrzeigersinn, false: gegen Uhrzeigersinn
    private float currentRotationValue;
    private CurrentCombination combination = new CurrentCombination();
    

    private bool _isSelected = false;
    private float _cumulatedRotation = 0.0f; 

    private float _lastFrameEulerRotation;
    


    //Wird beim Starten des Spiels einmalig initialisiert
    void Start()
    {
        _lastFrameEulerRotation = 90.0f;
        //GetComponent.hingeJoint..as.
        //rigidbody auf Sleep für 1 Sekunde

         
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
            //add +1 to list
            //set _cumulatedRotation = 0 and  _lastFrameEulerRotation = 90.0f;
            //freeze Button for a second in Ausgangsposition
            //some sound
        
        
        if(_cumulatedRotation > 360){
            combination.addCombiValue(+1);
            _cumulatedRotation = 0;
            _lastFrameEulerRotation = 90.0f;
            Debug.Log("360 Grad nach rechts gedreht");
            
            List<int> liste = combination.getCurrentCombination();
            string result = "List contents: ";
            foreach( var x in liste) {
                result += x.ToString() + ", ";
            };
            Debug.Log(result);

            //calls the disableRotation function
            StartCoroutine(disableRotation());

            

      
        } else if (_cumulatedRotation < -360){
            //add -1 to list
            //set _cumulatedRotation = 0 and  _lastFrameEulerRotation = 90.0f;
            //freeze Button for a second in Ausgangsposition
            //some sound


            combination.addCombiValue(-1);
            _cumulatedRotation = 0;
            _lastFrameEulerRotation = 90.0f;
            Debug.Log("360 Grad nach links gedreht");
            
            List<int> liste = combination.getCurrentCombination();
            string result = "List contents: ";
            foreach( var x in liste) {
                result += x.ToString() + ", ";
            };
            Debug.Log(result);

            //calls the disableRotation function
            StartCoroutine(disableRotation());
        }
     
    }


    //called by
    IEnumerator disableRotation(){
        
        HingeJoint hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;

        hinge.useLimits = true;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
        yield return new WaitForSeconds(2);
        hinge.useLimits = false;
        
        //adds constraints to RigidBody, doesnt work
        // rigid.constraints = RigidbodyConstraints.FreezeAll;
        // yield return new WaitForSeconds(15);
        // rigid.constraints = RigidbodyConstraints.None;
        
        //disables RigidBody, doesnt work
        // rigid.Sleep();
        // yield return new WaitForSeconds(2);
        // rigid.WakeUp();
       
        //adds limits to the hinge, does work!
        // hinge.useLimits = true;
        // yield return new WaitForSeconds(15);
        // hinge.useLimits = false;

        
       

        
        
        
     
    }

    //Unfertiger Prüfmechanismus, ob die letzte Rotation richtig war. Theoretische Logik:
    //1. Greife auf das letzte Element mit Index Länge - 1 zu.
    //2. Vergleiche Element mit Element der Lösungsliste an gleicher Stelle
    //3. Wenn richtig, mach nichts (oder gib die Liste zurück)
    //4. Wenn falsch, leere die Liste.
    //
    //Hier fehlt der Zugriff auf die extern gelagerte Liste, aktuell wird von einer internen Liste ausgegangen, auf die zugegriffen wird
    private List <int> isRotationCorrect (List<int> currentCombi)
    {
        if (CORRECT_COMBI[currentCombi.Count - 1] != currentCombi[currentCombi.Count - 1])
        {
            currentCombi.Clear();
            return currentCombi;
        }
        else if (currentCombi.Count == 6)
        {
            Debug.Log(currentCombi);
            //_doorOpen = true;
            return currentCombi;
        }
        else
        {
            return currentCombi;
        }
    }

    //private void checkCombi (List <int> currentCombi)
    //{
    //    if (currentCombi.Count >= 1 && currentCombi.Count <= 6)
    //    {
    //        if (isRotationCorrect(currentCombi))
    //            _doorOpen = true;
    //        else
    //            _rotationCombi.Clear();
    //    }
    //}

    // private bool GetRotateDirection(float from, float to)
    // {
    //     float clockWise = 0f;
    //     float counterClockWise = 0f;

    //     if (from <= to)
    //     {
    //         clockWise = to - from;
    //         counterClockWise = from + (360 - to);
    //     }
    //     else
    //     {
    //         clockWise = (360 - from) + to;
    //         counterClockWise = from - to;
    //     }
    //     // return (clockWise <= counterClockWise);
    // }




    //    gameObject.GetComponent<Rigidbody>().WakeUp();
    //    currentRotationValue = gameObject.transform.localEulerAngles.z;
    //        Debug.Log("currentRotationValue: " + currentRotationValue);


    //        //Debug.Log("CurrentRotationValue nach Abzug von FirstRotationValue: " + currentRotationValue);
    //        clockWise = GetRotateDirection(firstRotationValue, currentRotationValue);

    //        if(clockWise)
    //        {
    //            _rotationCombi.Add(1);
    //            Debug.Log("Eine Drehung im Uhrzeigersinn");
    //            Debug.Log("Eingegebene Kombination: " + _rotationCombi);
    //            //transform.Rotate(0f, 0f, (rotationSpeed * Time.deltaTime), 360.0f - currentRotationValue, Space.Self);
    //            to = new Vector3(0.0f, 0.0f, 360.0f - (float) currentRotationValue);
    //    gameObject.transform.eulerAngles = Vector3.Lerp(gameObject.transform.rotation.eulerAngles, to, Time.deltaTime);
    //            // checkCombi(_rotationCombi);
    //        }
    //        else
    //        {
    //            _rotationCombi.Add(-1);
    //            Debug.Log("Eine Drehung gegen den Uhrzeigersinn");
    //            to = new Vector3(0.0f, 0.0f, -(float) currentRotationValue);
    //gameObject.transform.eulerAngles = Vector3.Lerp(gameObject.transform.rotation.eulerAngles, to, Time.deltaTime);
    //            // checkCombi(_rotationCombi);
    //        }
    //        gameObject.GetComponent<Rigidbody>().Sleep();

        //Für den Fall, dass die Ausgangsrotation nicht 0 Grad ist:
        //    if (currentRotationValue >= firstRotationValue)
        //{
        //    currentRotationValue = currentRotationValue - firstRotationValue;
        //}
        //else
        //{
        //    currentRotationValue = 360.0f + currentRotationValue - firstRotationValue;
        //}


    //alte Update Funktion
    //     void Update ()
    // {
    
    //     if (clockWise)
    //     {
    //         //Drehung noch nicht fertig, weiterdrehen
    //         if (transform.localEulerAngles.z < 358)
    //         {
    //             transform.Rotate(0, 0, 1);
    //         }
    //         //Drehung fertig
    //         else
    //         {
    //             this.GetComponent<Rigidbody>().Sleep();
    //             //_rotationCombi.Add(1); -> Falls die Liste in diesem Skript gespeichert wird
    //             //Der Liste eine 1 für "im Uhrzeigersinn" hinzufügen
                
    //             combination.addCombiValue(1);
    //             Debug.Log("Aktuelle Kombination: " + combination.getCurrentCombination()); //Aktuelle Liste ausgeben
    //             //_rotationCombi = isRotationCorrect(_rotationCombi);
    //             Debug.Log("Tür offen? " + combination._doorOpen); //Türstatus ausgeben
    //             enabled = false; //Skript deaktivieren
    //         }
    //     }
    //     //Analog zu oben nur andersherum
    //     else
    //     {
    //         if (transform.localEulerAngles.z > 2)
    //         {
    //             transform.Rotate(0, 0, -1);
    //         }
    //         else
    //         {
    //             this.GetComponent<Rigidbody>().Sleep();
    //             //_rotationCombi.Add(-1);
    //             combination.addCombiValue(-1);
    //             Debug.Log("Aktuelle Kombination: " + combination.getCurrentCombination());
    //             //_rotationCombi = isRotationCorrect(_rotationCombi);
    //             Debug.Log("Tür offen? " + combination._doorOpen);
    //             enabled = false;
    //         }
    //     }
    // }
}
