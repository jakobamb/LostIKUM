using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTrigger : MonoBehaviour
{

    public RotationRedirector redirector;
    public bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHead>() != null)
        {
            Debug.Log("rotation trigger enter");
            if (!started)
            {
                redirector.StartRedirection();
                Debug.Log("rotatoin stated");
                started = true;
            }
        }
    }
}
