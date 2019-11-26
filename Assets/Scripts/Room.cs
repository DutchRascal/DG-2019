using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEnterred;
    public GameObject[] doors;

    private void Start()
    {
        // HandleDoors();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            HandleDoors();
        }
    }

    private void HandleDoors()
    {
        print("Handle Door");
        if (closeWhenEnterred)
        {
            foreach (GameObject door in doors)
            {
                print("Loop...");
                door.SetActive(true);
            }
        }
    }
}
