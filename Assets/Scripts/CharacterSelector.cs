﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool canSelect;

    public GameObject message;
    public PlayerController playerToSpawn;

    private void Update()
    {
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPosition = PlayerController.instance.transform.position;
                Destroy(PlayerController.instance.gameObject);
                PlayerController newPlayer = Instantiate(playerToSpawn, playerPosition, playerToSpawn.transform.rotation);
                PlayerController.instance = newPlayer;
                gameObject.SetActive(false);
                CameraController.instance.target = newPlayer.transform;
                CharacterSelectManager.instance.activePlayer = newPlayer;
                CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true);
                CharacterSelectManager.instance.activeCharSelect = this;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canSelect = false;
        message.SetActive(false);
    }
}
