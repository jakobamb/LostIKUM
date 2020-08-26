using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCombination
{
    private List <int> currentCombi = new List <int>();

    //Füge ein neues Element ans Ende der Liste an
    public void addCombiValue (int newValue)
    {
        currentCombi.Add(newValue);
    }

    //leert die Liste
    public void clearCombi () {
        currentCombi.Clear();
    }

    //Gib die Liste zurück
    public List <int> getCurrentCombination ()
    {
        return currentCombi;
    }
}
