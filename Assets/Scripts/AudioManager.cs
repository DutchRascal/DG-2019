using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource
            leveMusic,
            gameOverMusic,
            winMusic;
    public AudioSource[] sfx;

    private void Awake()
    {
        instance = this;
    }

    public void PlayLevelWin()
    {
        leveMusic.Stop();
        winMusic.Play();
    }

    public void PlayGameOver()
    {
        leveMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
}
