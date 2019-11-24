using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{

    private GameObject
            brokenPiece,
            dropItem;
    private int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[]
            brokenPieces,
            itemsToDrop;
    public float itemDropPercentage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player" && PlayerController.instance.isDashing) || other.tag == "PlayerBullet")
        {
            Smash();
        }
    }

    private void Smash()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(0);
        //show broken pieces
        for (int i = 0; i < Random.Range(1, maxPieces); i++)
        {
            brokenPiece = brokenPieces[Random.Range(0, brokenPieces.Length)];
            Instantiate(brokenPiece, transform.position, transform.rotation);
        }
        //drop items
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < itemDropPercentage)
            {
                dropItem = itemsToDrop[Random.Range(0, itemsToDrop.Length)];
                Instantiate(dropItem, transform.position, transform.rotation);
            }
        }
    }
}
