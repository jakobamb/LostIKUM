using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlossSchrank : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject mirrorDoorLeft;
    public GameObject mirrorDoorRight;
    HingeJoint hingeDL;
    HingeJoint hingeDR;
    HingeJoint hingeMDL;
    HingeJoint hingeMDR;
    
    public bool locked;
    
    public string test;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ActivateLimits(doorLeft);
        ActivateLimits(doorRight);
        ActivateLimits(mirrorDoorLeft);
        ActivateLimits(mirrorDoorRight);
        
        if(locked)
        {
            LockSchrank();
        }
        else
        {
            UnlockSchrank();
        }
   
    }
    
    private void ActivateLimits(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        hinge.useLimits = true;
    }
    
    
    void LockSchrank()
    {
        LockDoor(doorLeft);
        LockDoor(doorRight);
        LockDoor(mirrorDoorLeft);
        LockDoor(mirrorDoorRight);
    }
    
    void UnlockSchrank()
    {
        UnlockDoor(doorLeft);
        UnlockDoor(doorRight);
        UnlockDoor(mirrorDoorLeft);
        UnlockDoor(mirrorDoorRight);
    }
    
    void LockDoor(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
    }
    
    void UnlockDoor(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        test = door.name;
        if(door.name.Contains("Left"))
        {
            limits.max = 100;
        }
        else if(door.name.Contains("Right"))
        {
            limits.min = -100;
        }
        
        hinge.limits = limits;
    }
}
