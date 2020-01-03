﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text
            healthText,
            coinText,
            enemyText;
    public GameObject
            pauseMenu,
            deathScreen,
            mapDisplay,
            bigMapText;
    public Image fadeScreen;
    public float fadeSpeed;
    public bool
            fadeToBlack,
            fadeOutBlack;
    public string
            newGameScene,
            mainMenuScene;
    public Image currentGun;
    public Text gunText;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (deathScreen)
            deathScreen.SetActive(false);
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    void Update()
    {
        if (fadeScreen)
        {
            if (fadeOutBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    fadeOutBlack = false;
                }
            }

            if (fadeToBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 1f)
                {
                    fadeToBlack = false;
                }
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);

    }

    public void Resume()
    {
        LevelManager.instance.HandlePause();
    }
}
