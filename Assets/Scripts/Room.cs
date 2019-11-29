using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool
            closeWhenEnterred,
            openWhenEnemiesCleared;
    public GameObject[] doors;
    public List<GameObject> enemies = new List<GameObject>();
    private bool roomActive = false;

    private void Update()
    {
        print(enemies.Count);
        if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                    closeWhenEnterred = false;
                }

            }
        }
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
        if (closeWhenEnterred)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
        roomActive = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            roomActive = false;
    }
}
