using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCombination : MonoBehaviour
{
    public bool _doorOpen;
    private List <int> currentCombi = new List <int>();

    // Start is called before the first frame update
    void Start()
    {
        _doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Füge ein neues Element ans Ende der Liste an
    public void addCombiValue (int newValue)
    {
        currentCombi.Add(newValue);
    }

    //Gib die Liste zurück
    public List <int> getCurrentCombination ()
    {
        return currentCombi;
    }

    //Gib den letzten Wert der Liste zurück
    public int getLastValue()
    {
        return currentCombi[currentCombi.Count - 1];
    }

    //Wird aufgerufen, wenn die letzte Rotation richtig war
    public void rotationCorrect()
    {
        //Wenn insgesamt 6 Rotationen vorgenommen wurden, muss die ganze Kombi stimmen
        if (currentCombi.Count == 6)
        {
            _doorOpen = true;
        }
    }

    //Wird aufgerufen, wenn die letzte Rotation falsch war
    public void rotationFalse()
    {
        currentCombi.Clear();
    }
}
