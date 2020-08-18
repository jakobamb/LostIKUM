using System;
using UnityEngine;

/// Coordinates transitions of a room from and to activation and deactivation.
/// Used for e.g. animating room doors, playing sounds etc.
public class OverlappingRoomTransition : MonoBehaviour
{
    public event EventHandler<bool> RaiseTransitionStart;
    public event EventHandler<bool> RaiseTransitionEnd;

    public bool targetStateActive;

    /// Called by the redirector when a transition should begin.
    /// Register a handler for the `RaiseTransitionStart` event from
    /// another script to react to this.
    public void OnTransitionStart(bool active)
    {
        this.targetStateActive = active;
        RaiseTransitionStart(this, active);
    }

    /// Notify the redirector that the transition is finished.
    /// This will make the redirector actually show or hide the room, depending on the 
    /// target state last passed to `OnTransitionStart`.
    public void EndTransition()
    {
        RaiseTransitionEnd(this, targetStateActive);
    }

}