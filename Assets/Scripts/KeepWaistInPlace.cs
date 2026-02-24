using UnityEngine;

public class KeepWaistInPlace : MonoBehaviour
{
    public Transform xrCamera;     // Assign Main Camera
    public float waistOffset = 0.5f; // Adjust for player height

    void LateUpdate()
    {
        Vector3 camPos = xrCamera.position;
        transform.position = new Vector3(camPos.x, camPos.y - waistOffset, camPos.z);

        float yaw = xrCamera.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
