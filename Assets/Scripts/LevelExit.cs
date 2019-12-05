﻿using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canMove = false;
            AudioManager.instance.PlayLevelWin();
            StartCoroutine(LevelManager.instance.levelEnd());
        }
    }
}
