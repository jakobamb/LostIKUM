using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO delete this class in the future.

/// <summary>
/// Not used at the moment. Remains of the GUI of the student project before SpaceExtenderTool. Will be deleted in future versions.
/// </summary>
public class ToggleRedirector : MonoBehaviour
{

    // Variables for the custom inspector
    #if UNITY_EDITOR
     //index of the currently selected button for choosing which values to compute
    [SerializeField]
    int currentButtonSelection;


    #endif
    // Variables for custom inspector as well as this script
    [SerializeField]
    float gainValue; 
    [SerializeField]
    float realValue; 
    [SerializeField]
    float virtualValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
