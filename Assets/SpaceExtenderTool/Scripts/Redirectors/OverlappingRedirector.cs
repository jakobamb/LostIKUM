using UnityEngine;
using System.Collections.Generic;

public class OverlappingRedirector : MonoBehaviour
{
    public GameObject objectToHide;

    public bool playerInInterior;

    public bool playerInTransition;

    public OverlappingRoomTransition transition;

    public HashSet<GameObject> objectsInRoom = new HashSet<GameObject>();

    public bool showAreaGizmos;
    public CompoundTriggerEvents transitionArea;
    public CompoundTriggerEvents interiorArea;

    protected void Start()
    {
        var transitionArea = transform.Find("TransitionArea").gameObject.GetComponent<CompoundTriggerEvents>();
        transitionArea.RaiseTriggerEnter += OnColliderEnterTransition;
        transitionArea.RaiseTriggerExit += OnColliderExitTransition;
        var interiorArea = transform.Find("InteriorArea").gameObject.GetComponent<CompoundTriggerEvents>();
        interiorArea.RaiseTriggerEnter += OnColliderEnterInterior;
        interiorArea.RaiseTriggerExit += OnColliderExitInterior;
        playerInInterior = false;
        objectToHide.SetActive(false);
        if (transition != null) {
            transition.RaiseTransitionEnd += OnTransitionEnd;
        }
    }

    private void OnColliderEnterInterior(object sender, CollisionEventArgs args)
    {
        var obj = args.collider.gameObject;
        if (obj.TryGetComponent<OverlappingRedirectorPlayer>(out var player))
        {
            // Debug.Log(name + " enter interior");
            playerInInterior = true;
            player.OnPlayerEnterInterior(this);
        }
        else
        {
            if (obj.GetComponent<Rigidbody>() != null && 
                    !obj.isStatic &&
                    (!obj.TryGetComponent<IsInRoom>(out var isInRoom) || isInRoom.willBeRemoved) &&
                    objectToHide.activeInHierarchy)
            {
                // Debug.Log(args.collider.name + " added to object list");
                obj.AddComponent<IsInRoom>();
                objectsInRoom.Add(obj);
            } else {
                // Debug.Log(args.collider.name + " is already in room: " + obj.TryGetComponent<IsInRoom>(out var _));
            }
        }
    }

    private void OnColliderExitInterior(object sender, CollisionEventArgs args)
    {
        var obj = args.collider.gameObject;
        if (obj.TryGetComponent<OverlappingRedirectorPlayer>(out var player))
        {
            // Debug.Log(name + " exit interior");
            playerInInterior = false;
            player.OnPlayerExitInterior(this);
        }
        else
        {
            Debug.Log(args.collider.name + " exit " + name);
            if (obj.TryGetComponent<IsInRoom>(out var isInRoom) && objectToHide.activeInHierarchy)
            {
                isInRoom.willBeRemoved = true;
                Destroy(isInRoom);
                objectsInRoom.Remove(obj);
            }
        }
    }

    private void OnColliderEnterTransition(object sender, CollisionEventArgs args)
    {
        if (args.collider.gameObject.TryGetComponent<OverlappingRedirectorPlayer>(out var player))
        {
            // Debug.Log(name + " enter transition");
            playerInTransition = true;
            player.OnPlayerEnterTransition(this);
        }
    }

    private void OnColliderExitTransition(object sender, CollisionEventArgs args)
    {
        if (args.collider.gameObject.TryGetComponent<OverlappingRedirectorPlayer>(out var player))
        {
            // Debug.Log(name + " exit transition");
            playerInTransition = false;
            player.OnPlayerExitTransition(this);
        }
    }

    public void SetRoomActive(bool active)
    {
        if (transition != null) {
            transition.OnTransitionStart(active);

            // Wait for the transition to finish before doing anything
            return;
        }

        SetRoomActiveInstantly(active);
    }

    public void SetRoomActiveInstantly(bool active)
    {
        objectToHide.SetActive(active);

        foreach (GameObject obj in objectsInRoom)
        {
            if (!obj.GetComponent<Rigidbody>().isKinematic)
            {
                // Debug.Log(obj.name + " " + active);
                obj.SetActive(active);
            }
        }
    }

    public void OnTransitionEnd(object sender, bool active)
    {
        SetRoomActiveInstantly(active);
    }

    private void OnDrawGizmos()
    {
        if (showAreaGizmos)
        {
            if (transitionArea != null)
                DrawAreaGizmo(transitionArea.gameObject, new Color(0, 0, 0, 0.3f));
            if (interiorArea != null)
                DrawAreaGizmo(interiorArea.gameObject, new Color(0, 0, 0, 0.8f));
        }
    }

    private void DrawAreaGizmo(GameObject area, Color color)
    {
        if(area != null)
        {
            List<Collider> colliders = GetChildColliders(area.transform);
            foreach (Collider c in colliders)
            {
                Gizmos.color = color;
                Gizmos.DrawCube(c.bounds.center, c.bounds.extents * 2);
                Gizmos.DrawWireCube(c.bounds.center, c.bounds.extents * 2);
            }
        }
    }

    private List<Collider> GetChildColliders(Transform transformToSearch)
    {
        List<Collider> result = new List<Collider>();
        result.AddRange(transformToSearch.GetComponents<Collider>());        
        for (int i = 0; i < transformToSearch.childCount; i++)
        {
            result.AddRange(GetChildColliders(transformToSearch.GetChild(i)));
        }
        return result;
    }
}