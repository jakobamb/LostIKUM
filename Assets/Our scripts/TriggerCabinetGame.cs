using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCabinetGame: MonoBehaviour
{

    public SchrankTransitionMgr transitionMgr;
    public bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!started && other.tag == "MainCamera")
        {
            transitionMgr.CabinetTriggerEnter();
            Debug.Log("[TriggerCabinetGame] Cabinet game stated");
            started = true;
        }
    }
}