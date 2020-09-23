using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_rotation: MonoBehaviour
{

    public RotationRedirector redirector;
    public bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.gameObject.CompareTag("MainCamera"))
        {
            if (!started)
            {
                redirector.StartRedirection();
                Debug.Log("[trigger_rotation] rotation stated");
                started = true;
            }
        }
    }
}