using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class SnapScannerToPlace : MonoBehaviour
{
    public Transform waistPoint;
    public GameObject item;
    public XRGrabInteractable grab;
    
    Vector3 positionOffset;


    [Header("Offsets")]
    public bool rightHanded = true;
    public Vector3 rotationOffset = new(0f, 90f, 0f);

    void Start()
    {
        if (rightHanded)
        {
            positionOffset = new(0.3f, 0f, 0f);
        }
        else
        {
            positionOffset = new(-0.3f, 0f, 0f);
        }
        item.transform.SetParent(waistPoint);
        item.transform.SetLocalPositionAndRotation(positionOffset, Quaternion.Euler(rotationOffset));
    }
    void LateUpdate()
    {
        if (!grab.isSelected)
        {
            item.transform.SetLocalPositionAndRotation(positionOffset, Quaternion.Euler(rotationOffset));            
        }
    }
}
