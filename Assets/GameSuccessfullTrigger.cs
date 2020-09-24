using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSuccessfullTrigger : MonoBehaviour
{
    public room_manager_script roomManager;
    public bool started = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!started && other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("[GameSuccessfullTrigger] activated");
            roomManager.triggerGameOverSuccess();
            started = true;
        }
    }
}
