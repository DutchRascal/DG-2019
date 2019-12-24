using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    public float waitToBeCollected = 0.2f;

    private void Start()
    {
        UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
    }
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
            LevelManager.instance.GetCoins(coinValue);
            UIController.instance.coinText.text = LevelManager.instance.currentCoins.ToString();
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
        }
    }
}
