using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class SnapToWaist : MonoBehaviour
{
    public Transform waistPoint;
    public GameObject item;
    public XRGrabInteractable grab;

    void Start()
    {
        item.transform.SetParent(waistPoint);
        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
    void LateUpdate()
    {
        if (!grab.isSelected)
        {
            item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);            
        }
    }
}
