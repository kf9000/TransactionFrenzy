using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class DisableHandCollidersOnGrab : MonoBehaviour
{
    GameObject[] hands;
    readonly List<Collider> allColliders = new();


    void Start()
    {
        hands = GameObject.FindGameObjectsWithTag("Hands");
        foreach (GameObject hand in hands)
        {
            Collider[] colliders = hand.GetComponentsInChildren<Collider>(true);
            allColliders.AddRange(colliders);
        }

    }

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        foreach (Collider c in allColliders)
        {
            c.enabled = false;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        foreach (Collider c in allColliders)
        {
            c.enabled = true;
        }
    }
}
