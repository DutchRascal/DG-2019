using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public static LevelExit instance;

    public Collider2D exitCollider;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "Character Select")
        {
            exitCollider.isTrigger = false;
        }
        if (SceneManager.GetActiveScene().name == "Boss 1")
        {
            exitCollider.isTrigger = true;
        }
    }

    public void updateExitCollider()
    {
        exitCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canMove = false;
            AudioManager.instance.PlayLevelWin();
            StartCoroutine(LevelManager.instance.levelEnd());
        }
    }
}
