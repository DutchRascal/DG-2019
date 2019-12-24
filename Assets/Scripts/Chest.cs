using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public static Chest instance;

    public GameObject
            coinsToShow,
            openChest;
    public float maxCoins = 5f;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(openChest, transform.position, transform.rotation);
            for (int i = 1; i < Random.Range(0, maxCoins); i++)
            {
                Instantiate(coinsToShow, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
