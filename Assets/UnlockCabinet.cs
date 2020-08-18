using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCabinet : MonoBehaviour
{
    public GameObject drawer;
    ConfigurableJoint drawerslide;
    // Start is called before the first frame update
    void Start()
    {
        drawerslide = drawer.GetComponent<ConfigurableJoint>();
        drawerslide.xMotion = ConfigurableJointMotion.Locked;
    }

    
    private void OnTriggerEnter(Collider key)
    {   
        if(key.gameObject.CompareTag("CabinetKey"))
        {
            drawerslide = drawer.GetComponent<ConfigurableJoint>();
            drawerslide.xMotion = ConfigurableJointMotion.Limited;
        }
    }
}
