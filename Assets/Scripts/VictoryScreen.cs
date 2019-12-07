using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{

    public float waitForAnyKey = 2f;
    public GameObject anyKeyText;
    public string mainMenuScreen;

    private void Start()
    {
        anyKeyText.SetActive(false);
    }

    private void Update()
    {
        if (waitForAnyKey > 0)
        {
            waitForAnyKey -= Time.deltaTime;
            if (waitForAnyKey <= 0)
                anyKeyText.SetActive(true);
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScreen);
            }
        }
    }
}
