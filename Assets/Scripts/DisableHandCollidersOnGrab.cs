using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class DisableHandCollidersOnGrab : MonoBehaviour
{
    GameObject rightHand;
    GameObject leftHand;
    readonly List<Collider> rightColliders = new();
    readonly List<Collider> leftColliders = new();



    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        Collider[] collidersTempRight = rightHand.GetComponentsInChildren<Collider>(true);
        rightColliders.AddRange(collidersTempRight);

        leftHand = GameObject.FindGameObjectWithTag("LeftHand");

        Collider[] collidersTempLeft = leftHand.GetComponentsInChildren<Collider>(true);
        leftColliders.AddRange(collidersTempLeft);


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
        var interactor = args.interactorObject;
        GameObject controllerObj = interactor.transform.gameObject;
        var xrController = controllerObj.GetComponent<NearFarInteractor>();
        if(xrController.handedness.ToString() == "Right")
        {
            foreach (Collider c in rightColliders)
            {
                c.enabled = false;
            }            
        }
        else if (xrController.handedness.ToString() == "Left")
        {
            foreach (Collider c in leftColliders)
            {
                c.enabled = false;
            }           
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;
        GameObject controllerObj = interactor.transform.gameObject;
        var xrController = controllerObj.GetComponent<NearFarInteractor>();
        if(xrController.handedness.ToString() == "Right")
        {
            foreach (Collider c in rightColliders)
            {
                c.enabled = true;
            }            
        }
        else if (xrController.handedness.ToString() == "Left")
        {
            foreach (Collider c in leftColliders)
            {
                c.enabled = true;
            }           
        }

    }
}
