using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

    public void OpenOptions()
    {
        // Implement this according to your needs.
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
