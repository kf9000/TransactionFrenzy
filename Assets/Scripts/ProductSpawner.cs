using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public List<GameObject> products;
    public AudioSource SpawnSound;

    public void SpawnProduct()
    {
        SpawnSound.Play();
        Instantiate(products[Random.Range(0,products.Count)], transform.position, GetRandomRotation());
    }

    Quaternion GetRandomRotation()
    {
        Vector3[] axes = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        List<Quaternion> rotations = new();

        foreach (Vector3 up in axes)
        {
            foreach (Vector3 forward in axes)
            {
                if(Vector3.Dot(up, forward) == 0)
                {
                    rotations.Add(Quaternion.LookRotation(forward, up));
                }
            }
        }

        return rotations[Random.Range(0, rotations.Count())];
    }
}
