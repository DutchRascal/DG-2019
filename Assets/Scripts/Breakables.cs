using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{

    public GameObject[] brokenPieces;
    private GameObject brokenPiece;
    private int maxPieces = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.isDashing)
        {
            Destroy(gameObject);
            for (int i = 0; i < Random.Range(1, maxPieces); i++)
            {
                brokenPiece = brokenPieces[Random.Range(0, brokenPieces.Length)];
                Instantiate(brokenPiece, transform.position, transform.rotation);
            }
        }
    }
}
