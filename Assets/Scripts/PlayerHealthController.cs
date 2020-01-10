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
        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth;
        UIController.instance.UpdateUIElements();
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
            AudioManager.instance.PlaySFX(11);
            invincibleCount = damageInvincibleLength;
            PlayerController.instance.bodySR.color = new Color(rColor, gColor, bColor, .5f);
            UIController.instance.UpdateUIElements();
            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySFX(9);
                PlayerController.instance.gameObject.SetActive(false);
                StartCoroutine(GameOver());
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        UIController.instance.deathScreen.SetActive(true);
        AudioManager.instance.PlayGameOver();

    }

    public void MakeInvisible(float length)
    {
        invincibleCount = length;
        PlayerController.instance.bodySR.color = new Color(rColor, gColor, bColor, .5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateUIElements();
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        UIController.instance.UpdateUIElements();
    }
}
