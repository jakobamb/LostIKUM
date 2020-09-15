using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SchrankTransitionMgr : MonoBehaviour
{
    // references to game objects
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject mirrorDoorLeft;
    public GameObject mirrorDoorRight;

    public GameObject schrankGame;

    // Events
    public UnityEvent onDoorClosing;
    public UnityEvent onDoorUnlocked;

    // status fields
    public bool nonMirrorSideLocked;
    public bool mirrorSideLocked;

    void Awake()
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

            // disable the Schrank Game initially.
            schrankGame.SetActive(false);

        } else if (newState == TransitionState.enableToMirror)
        {
            onDoorUnlocked.Invoke();
            UnlockNonMirrorSide();
            schrankGame.SetActive(true);
        } else if (newState == TransitionState.duringToMirror)
        {
            // close doors and lock all
            onDoorClosing.Invoke();
            LockMirrorSide();
            LockNonMirrorSide();
        } else if (newState == TransitionState.afterToMirror)
        {
            onDoorUnlocked.Invoke();
            UnlockMirrorSide();
        }
    }

    public void DrehschlossUnlocked()
    {
        UpdateState(TransitionState.enableToMirror);
    }

    public void CabinetTriggerEnter()
    {
        UpdateState(TransitionState.duringToMirror);
    }

    public void ToMirrorGameFinished()
    {
        UpdateState(TransitionState.afterToMirror);
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
        mirrorSideLocked = true;
        mirrorDoorLeft.GetComponent<HingeJoint>().useSpring = true;
        mirrorDoorRight.GetComponent<HingeJoint>().useSpring = true;
        LockDoor(mirrorDoorRight);
        LockDoor(mirrorDoorLeft);
    }

    private void LockNonMirrorSide()
    {
        nonMirrorSideLocked = true;
        doorLeft.GetComponent<HingeJoint>().useSpring = true;
        doorRight.GetComponent<HingeJoint>().useSpring = true;
        LockDoor(doorLeft);
        LockDoor(doorRight);
    }
    private void UnlockMirrorSide()
    {
        mirrorSideLocked = false;
        mirrorDoorLeft.GetComponent<HingeJoint>().useSpring = false;
        mirrorDoorRight.GetComponent<HingeJoint>().useSpring = false;
        UnlockDoor(mirrorDoorLeft);
        UnlockDoor(mirrorDoorRight);
    }

    private void UnlockNonMirrorSide()
    {
        nonMirrorSideLocked = false;
        doorLeft.GetComponent<HingeJoint>().useSpring = false;
        doorRight.GetComponent<HingeJoint>().useSpring = false;
        UnlockDoor(doorLeft);
        UnlockDoor(doorRight);
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