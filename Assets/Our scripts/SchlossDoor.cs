using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlossDoor : MonoBehaviour
{

    HingeJoint hinge;
    public bool locked;

    // Start is called before the first frame update
    protected void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useLimits = true;
        UnlockDoor();

    }

    void Update(){
        if(locked){
            LockDoor();
        } else {
            UnlockDoor();
        }
    }

    

    private void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag("Hauptkey"))
            {
                UnlockDoor();
            }
        }
        
    private void LockDoor(){
        hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
        locked = true;
        
    }
    private void UnlockDoor(){
        hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.max = 100;
        hinge.limits = limits;
        locked = false;
    }
}

