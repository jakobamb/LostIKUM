using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_rotation: MonoBehaviour
{

    public RotationRedirector redirector;
    public bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("rotation trigger enter");
        if (!started)
        {
            redirector.StartRedirection();
            Debug.Log("rotation stated");
            started = true;
        }
    }
}
