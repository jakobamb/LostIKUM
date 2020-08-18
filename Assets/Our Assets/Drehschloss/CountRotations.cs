using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRotations : MonoBehaviour
{
    public List<int> CORRECT_COMBI = new List<int>() { 1, 1, -1, 1, 1, 1 };
    //public bool _doorOpen = false;

    private bool clockWise;
    public float firstRotationValue = 0;
    private float currentRotationValue;   
    private static GameObject combinationObject;
    private CurrentCombination combination;
    //private List <int> _rotationCombi = new List<int>(); -> Falls die Liste intern gespeichert werden soll

    //Wird beim Loslassen des Zylinders aufgerufen
    public void OnRelease()
    {
        currentRotationValue = transform.localEulerAngles.z; //bestimmt aktuelle Rotation
        clockWise = GetRotateDirection(0.0f, currentRotationValue); //bestimmt, ob die Rotation im oder gegen den Uhrzeigersinn war
    }

    // Wird beim Starten des Spiels einmalig initialisiert
    void Start()
    {
        combinationObject = GameObject.FindWithTag("CurrentCombination"); //Ordne das externe GameObject zu

        //Speichere das externe Skript vom GameObject zwischen, um späteren Zugriff zu erleichtern       
        combination = combinationObject.GetComponent("CurrentCombination") as CurrentCombination; 
    }

    //Wird in jedem Frame aufgerufen, nachdem das Skript durch Loslassen des Zylinders gestartet wurde.
    //Je nach dem, ob der Zylinder im oder gegen den Uhrzeigersinn gedreht wurde, wird die Drehung in die gleiche Richtung vollendet,
    //bis der Strich wieder oben ist. Sobald die Drehung vollendet ist, wird der List im externen GameObject eine 1 oder -1 hinzugefügt. 
    void Update ()
    {
        if (clockWise)
        {
            //Drehung noch nicht fertig, weiterdrehen
            if (transform.localEulerAngles.z < 358)
            {
                transform.Rotate(0, 0, 1);
            }
            //Drehung fertig
            else
            {
                this.GetComponent<Rigidbody>().Sleep();
                //_rotationCombi.Add(1); -> Falls die Liste in diesem Skript gespeichert wird
                //Der Liste eine 1 für "im Uhrzeigersinn" hinzufügen
                combination.addCombiValue(1);
                Debug.Log("Aktuelle Kombination: " + combination.getCurrentCombination()); //Aktuelle Liste ausgeben
                //_rotationCombi = isRotationCorrect(_rotationCombi);
                Debug.Log("Tür offen? " + combination._doorOpen); //Türstatus ausgeben
                enabled = false; //Skript deaktivieren
            }
        }
        //Analog zu oben nur andersherum
        else
        {
            if (transform.localEulerAngles.z > 2)
            {
                transform.Rotate(0, 0, -1);
            }
            else
            {
                this.GetComponent<Rigidbody>().Sleep();
                //_rotationCombi.Add(-1);
                combination.addCombiValue(-1);
                Debug.Log("Aktuelle Kombination: " + combination.getCurrentCombination());
                //_rotationCombi = isRotationCorrect(_rotationCombi);
                Debug.Log("Tür offen? " + combination._doorOpen);
                enabled = false;
            }
        }
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

    private bool GetRotateDirection(float from, float to)
    {
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (from <= to)
        {
            clockWise = to - from;
            counterClockWise = from + (360 - to);
        }
        else
        {
            clockWise = (360 - from) + to;
            counterClockWise = from - to;
        }
        return (clockWise <= counterClockWise);
    }
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
}
