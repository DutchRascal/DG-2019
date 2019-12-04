using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            AudioManager.instance.PlayLevelWin();
            StartCoroutine(LevelManager.instance.levelEnd());
        }
    }
}
