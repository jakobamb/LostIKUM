using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrankTransitionMgr : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject mirrorDoorLeft;
    public GameObject mirrorDoorRight;

    public bool nonMirrorSideLocked;
    public bool mirrorSideLocked;

    public trigger_rotation rotationTrigger;

    private TransitionState currentState = TransitionState.locked;

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(TransitionState.locked);
    }


    private void UpdateState(TransitionState newState)
    {
        if (newState == TransitionState.locked)
        {
            // lock all doors
            ActivateLimits(doorLeft);
            ActivateLimits(doorRight);
            ActivateLimits(mirrorDoorLeft);
            ActivateLimits(mirrorDoorRight);
            if (mirrorSideLocked) {LockMirrorSide();} else {UnlockMirrorSide();}
            if (nonMirrorSideLocked) {LockNonMirrorSide();} else {UnlockNonMirrorSide();}

            // disable rotation trigger
            rotationTrigger.activated = false;
        }
    }

    void LockDoor(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;
        limits.min = 0;
        limits.max = 0;
        hinge.limits = limits;
    }

    private void ActivateLimits(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        hinge.useLimits = true;
    }

    public void LockMirrorSide()
    {
        LockDoor(mirrorDoorLeft);
        LockDoor(mirrorDoorRight);
    }

    public void LockNonMirrorSide()
    {
        LockDoor(doorLeft);
        LockDoor(doorRight);
    }

    public void UnlockNonMirrorSide()
    {
        UnlockDoor(doorLeft);
        UnlockDoor(doorRight);
    }
    public void UnlockMirrorSide()
    {
        UnlockDoor(mirrorDoorLeft);
        UnlockDoor(mirrorDoorRight);
    }

    void UnlockDoor(GameObject door)
    {
        HingeJoint hinge = door.GetComponent<HingeJoint>();
        JointLimits limits = hinge.limits;

        if (door.name.Contains("Left"))
        {
            limits.max = 100;
        }
        else if (door.name.Contains("Right"))
        {
            limits.min = -100;
        }

        hinge.limits = limits;
    }
}

/**
 * These are the possible states during transition between the two rooms.
 * First, all doors are locked and triggers are disabled (locked)
 */
public enum TransitionState
{
    locked,
    enableToMirror,
    duringToMirror,
    afterToMirror,
    enableToOriginal,
    duringToOriginal,
    afterToOriginal
}