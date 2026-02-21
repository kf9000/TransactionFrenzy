using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject product;

    public void SpawnProduct()
    {
        Instantiate(product, transform);
    }
}
