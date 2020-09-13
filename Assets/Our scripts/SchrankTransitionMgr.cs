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

    public GameObject schrankGame;

    public trigger_rotation rotationTrigger;

    private TransitionState currentState = TransitionState.initial;

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(TransitionState.initial);
    }
    private void UpdateState(TransitionState newState)
    {
        Debug.Log("[TransitionStateMgr]: newState:" + newState.ToString());

        if (newState == TransitionState.initial)
        {
            // lock all doors
            ActivateLimits(doorLeft);
            ActivateLimits(doorRight);
            ActivateLimits(mirrorDoorLeft);
            ActivateLimits(mirrorDoorRight);
            LockMirrorSide();
            LockNonMirrorSide();

            // disable rotation trigger
            rotationTrigger.activated = false;

            // disable the Schrank Game initially.
            schrankGame.SetActive(false);

        } else if (newState == TransitionState.enableToMirror)
        {
            UnlockNonMirrorSide();
            schrankGame.SetActive(true);
        }
    }

    public void DrehschlossUnlocked()
    {
        UpdateState(TransitionState.enableToMirror);
    }

    private void LockDoor(GameObject door)
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

    private void LockMirrorSide()
    {
        LockDoor(mirrorDoorLeft);
        LockDoor(mirrorDoorRight);
    }

    private void LockNonMirrorSide()
    {
        LockDoor(doorLeft);
        LockDoor(doorRight);
    }

    private void UnlockNonMirrorSide()
    {
        UnlockDoor(doorLeft);
        UnlockDoor(doorRight);
    }
    private void UnlockMirrorSide()
    {
        UnlockDoor(mirrorDoorLeft);
        UnlockDoor(mirrorDoorRight);
    }

    private void UnlockDoor(GameObject door)
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
    initial,
    enableToMirror,
    duringToMirror,
    afterToMirror,
    enableToOriginal,
    duringToOriginal,
    afterToOriginal
}