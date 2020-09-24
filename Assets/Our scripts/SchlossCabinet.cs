using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SchlossCabinet : MonoBehaviour
{
    ConfigurableJoint drawerslide;

    [TagSelector]
    public string keyTag = "";

    public bool locked;

    void Update()
    {
        if(locked)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag(keyTag))
        {
            Unlock();
            other.GetComponent<XRGrabInteractableInherit>().enabled = false;

        }
    }
    
    private void Lock()
    {
        drawerslide = GetComponent<ConfigurableJoint>();
        drawerslide.xMotion = ConfigurableJointMotion.Locked;
        locked = true;
    }
    
    private void Unlock()
    {
        drawerslide = GetComponent<ConfigurableJoint>();
        drawerslide.xMotion = ConfigurableJointMotion.Limited;
        locked = false;
    }
}