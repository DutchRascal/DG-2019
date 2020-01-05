using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject
            coinsToShow,
            openChest;
    public GunPickup[] potentialGuns;
    public Transform spawnPoint;

    public float scaleSpeed = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            int gunSelect = Random.Range(0, potentialGuns.Length);
            Vector3 gunPosition = new Vector3(transform.position.x + Random.Range(1, 1), transform.position.y + Random.Range(1, 1), transform.position.z);
            Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation);
            Instantiate(openChest, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
