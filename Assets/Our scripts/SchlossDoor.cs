using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SchlossDoor : MonoBehaviour
{
    public UnityEvent playLockedSound;
    public UnityEvent playUnlockSound;
    HingeJoint hinge;
    public bool locked;

    [TagSelector]
    public string keyTag = "";

    // Start is called before the first frame update
    protected void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useLimits = true;
    }

    void Update(){
        if(locked){
            LockDoor();
        } else {
            UnlockDoor();
        }
    }


    public void onOpenAttempt()
    {
        if (locked)
        {
            playLockedSound.Invoke();
        }
    }
    

    private void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag(keyTag))
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
        playUnlockSound.Invoke();
        hinge = GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.max = 100;
        hinge.limits = limits;
        locked = false;
    }
}

