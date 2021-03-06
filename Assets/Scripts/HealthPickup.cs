﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private float waitToBeCollected = 0.5f;

    public int healAmount = 1;

    private void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);
        }
    }
}
