using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public static Chest instance;

    public GameObject
            coinsToShow,
            openChest;
    public int numberOfCoins;

    [SerializeField] private float maxCoins = 25;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            numberOfCoins = (int)Random.Range(0, maxCoins);
            print(numberOfCoins);
            Instantiate(openChest, transform.position, transform.rotation);
            for (int i = 0; i < numberOfCoins; i++)
            {
                Vector3 coinPosition = new Vector3(transform.position.x + Random.Range(1, 1), transform.position.y + Random.Range(1, 1), transform.position.z);
                Instantiate(coinsToShow, coinPosition, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
