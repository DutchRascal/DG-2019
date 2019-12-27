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
    public int
            itemCost,
            healthUpgradeAmount;

    private void Start()
    {
        //     PlayerController.instance.allowedToShoot = false;
    }

    private void OnDisable()
    {
        // PlayerController.instance.allowedToShoot = true;
    }

    private void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                {
                    if (LevelManager.instance.currentCoins >= itemCost)
                    {
                        LevelManager.instance.SpendCoins(itemCost);
                        // if (isHealthRestore && PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
                        if (isHealthRestore)
                        {
                            PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                        }
                        if (isHealthUpgrade)
                        {
                            PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                        }
                        gameObject.SetActive(false);
                        inBuyZone = false;
                        AudioManager.instance.PlaySFX(18);
                    }
                    else
                        AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
