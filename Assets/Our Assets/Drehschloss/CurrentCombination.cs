using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCombination
{
    public bool _doorOpen = false;
    private List <int> currentCombi = new List <int>();

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
