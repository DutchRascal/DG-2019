using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUIElements();
    }

    private void UpdateUIElements()
    {
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    void Update()
    {

    }

    public void DamagePlayer()
    {
        currentHealth--;
        UpdateUIElements();
        if (currentHealth <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
            UIController.instance.deathScreen.SetActive(true);
        }
    }
}
