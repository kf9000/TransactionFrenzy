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
    public AudioSource Beep;
    LayerMask Mask;


    void Awake()
    {
    grab.activated.AddListener(Activated);
    grab.deactivated.AddListener(Deactivated);
    Mask = LayerMask.GetMask("Barcode");

    }

    private void Deactivated(DeactivateEventArgs args)
    {
        ray.SetActive(false);

        if (grab.isSelected)
        {
            if (Physics.Raycast(castPoint.transform.position, castPoint.transform.forward, out RaycastHit hit, range, Mask))
            {
                if(hit.collider.CompareTag("Barcode"))
                {
                    Beep.Play();
                    hit.collider.tag = "Scanned";
                }
            }

        }
    }

    private void Activated(ActivateEventArgs args)
    {
        if (grab.isSelected)
        {
            ray.SetActive(true);
        }
    }

}
