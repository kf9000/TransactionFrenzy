using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Conveyor : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction = Vector3.right;
    
    [Header("Animation")]
    public float textureScrollSpeed = 2f;
    private Renderer rend;
    
    void Start()
        {
            rend = GetComponent<Renderer>();
        }

    void Update()
    {
        float offset = Time.time * textureScrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigidbody = collision.rigidbody;



        if(rigidbody != null)
        {
            rigidbody.linearVelocity = new Vector3(direction.x * speed, rigidbody.linearVelocity.y, direction.z * speed);
        }
    }
}
