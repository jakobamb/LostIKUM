using System;
using UnityEngine;

public class CollisionEventArgs : EventArgs
{
    public Collider collider {get; set;}

    public CollisionEventArgs(Collider other)
    {
        collider = other;
    }
}