using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class ProductRemover : MonoBehaviour
{
    public UnityEvent endGame;
    public bool canAdvanceScore = true;
    public AudioSource DespawnSound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetNamedChild("Barcode") != null && collision.gameObject.GetNamedChild("Barcode").CompareTag("Barcode"))
        {
            endGame.Invoke();
            DespawnSound.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetNamedChild("Barcode") != null && collision.gameObject.GetNamedChild("Barcode").CompareTag("Scanned"))
        {
            if (canAdvanceScore)
            {
                Manager.score++;           
            }
            else
            {
                
                endGame.Invoke();
            }
            DespawnSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
