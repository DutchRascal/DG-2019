using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;
    public float damageInvincibleLength = 1f;
    private float invincibleCount;
    private float rColor;
    private float gColor;
    private float bColor;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUIElements();
        rColor = PlayerController.instance.bodySR.color.r;
        gColor = PlayerController.instance.bodySR.color.g;
        bColor = PlayerController.instance.bodySR.color.b;
    }

    void Update()
    {
        if (invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;
            if (invincibleCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(rColor, gColor, bColor, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (invincibleCount <= 0)
        {
            currentHealth--;
            invincibleCount = damageInvincibleLength;
            PlayerController.instance.bodySR.color = new Color(rColor, gColor, bColor, .5f);
            UpdateUIElements();
            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }
        }
    }

    private void UpdateUIElements()
    {
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    public void MakeInvisible(float length)
    {
        invincibleCount = length;
        PlayerController.instance.bodySR.color = new Color(rColor, gColor, bColor, .5f);
    }

    public void healPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateUIElements();
    }
}
