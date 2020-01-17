using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject deletePanel;
    public Button resetButton;

    private void Start()
    {
        deletePanel.SetActive(false);
        if (!PlayerPrefs.HasKey("UnlockedPlayers"))
        {
            resetButton.interactable = false;
        }
        else
        {
            resetButton.interactable = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        PlayerPrefs.DeleteAll();
        deletePanel.SetActive(false);
        resetButton.interactable = false;
    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
