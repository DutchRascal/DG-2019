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
        if (SceneManager.GetActiveScene().name != "Character Select")
            exitCollider.isTrigger = false;
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
