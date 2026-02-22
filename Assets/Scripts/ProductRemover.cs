using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class ProductRemover : MonoBehaviour
{
    public UnityEvent endGame;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetNamedChild("Barcode").CompareTag("Barcode"))
        {
            endGame.Invoke();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetNamedChild("Barcode").CompareTag("Scanned"))
        {
            Destroy(collision.gameObject);
        }
    }
}
