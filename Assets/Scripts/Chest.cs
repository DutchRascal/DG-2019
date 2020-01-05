using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject
            coinsToShow,
            openChest;
    public int numberOfCoins;
    public GunPickup[] potentialGuns;
    public Transform spawnPoint;

    [SerializeField] private float maxCoins = 25;
    public float scaleSpeed = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int randomNumber = Random.Range(0, 1000) % 2;
            if (randomNumber == 0)
            {
                numberOfCoins = (int)Random.Range(0, maxCoins);
                print(numberOfCoins);
            }
            else
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);
                Vector3 gunPosition = new Vector3(transform.position.x + Random.Range(1, 1), transform.position.y + Random.Range(1, 1), transform.position.z);
                // Instantiate(potentialGuns[gunSelect], gunPosition, transform.rotation);
                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);
            }
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
