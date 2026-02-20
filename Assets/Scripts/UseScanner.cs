using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class UseScanner : MonoBehaviour
{
    public XRGrabInteractable grab;
    public GameObject ray;
    public GameObject castPoint;
    public float range = 1;
    LayerMask ignoredMask;


    void Awake()
    {
    grab.activated.AddListener(Activated);
    grab.deactivated.AddListener(Deactivated);
    ignoredMask = LayerMask.GetMask("RayVisual");

    }

    private void Deactivated(DeactivateEventArgs args)
    {
        ray.SetActive(false);
    }

    private void Activated(ActivateEventArgs args)
    {
        if (grab.isSelected)
        {
            ray.SetActive(true);

            if (Physics.Raycast(castPoint.transform.position, castPoint.transform.forward, out RaycastHit hit, range, ~ignoredMask))
            {
                if(hit.collider.CompareTag("Barcode"))
                {
                    Debug.Log("Barcode hit!");
                }
            }

        }
    }

}
