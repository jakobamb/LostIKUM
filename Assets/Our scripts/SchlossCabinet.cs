using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SchlossCabinet : MonoBehaviour
{
    public GameObject drawer;
    ConfigurableJoint drawerslide;

    public bool locked;
    public string keyTag;
    // Start is called before the first frame update
    void Start()
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
        drawerslide = drawer.GetComponent<ConfigurableJoint>();
        drawerslide.xMotion = ConfigurableJointMotion.Locked;
        locked = true;
    }
    
    private void Unlock()
    {
        drawerslide = drawer.GetComponent<ConfigurableJoint>();
        drawerslide.xMotion = ConfigurableJointMotion.Limited;
        locked = false;
    }
}