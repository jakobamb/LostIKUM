using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{

    HingeJoint hinge;
    // Start is called before the first frame update
    protected void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        
        limits.min = 0;
        limits.max = 4;
        
        hinge.useLimits = true;
        hinge.limits = limits;
    }
    

    private void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag("Hauptkey"))
            {
                hinge = GetComponent<HingeJoint>();
                JointLimits limits = hinge.limits;
                
                limits.max = 160;
                hinge.limits = limits;
            }
        }
}

