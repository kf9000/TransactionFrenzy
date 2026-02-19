using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class UseScanner : MonoBehaviour
{
    public XRGrabInteractable grab;
    public GameObject Ray;

    void Awake()
    {
    grab.activated.AddListener(Activated);
    grab.deactivated.AddListener(Deactivated);

    }

    private void Deactivated(DeactivateEventArgs args)
    {
        Ray.SetActive(false);
    }

    private void Activated(ActivateEventArgs args)
    {
        if (grab.isSelected)
        {
            Ray.SetActive(true);
        }
    }

}
