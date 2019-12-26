using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{

    private bool inBuyZone;

    public GameObject buyMessage;
    public bool
            isHealthRestore,
            isHealthUpgrade,
            isWeapon;
    public int itemCost;

    private void Update()
    {
        if (inBuyZone && Input.GetKeyDown(KeyCode.E) && (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth))
        {
            if (LevelManager.instance.currentCoins >= itemCost)
            {
                LevelManager.instance.SpendCoins(itemCost);
                if (isHealthRestore)
                {
                    PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            buyMessage.SetActive(true);
        inBuyZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            buyMessage.SetActive(false);
        inBuyZone = false;
    }
}
