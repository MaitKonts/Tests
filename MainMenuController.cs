using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("WorldSelectionScene");
    }

    public void OpenMultiplayerMenu()
    {
        SceneManager.LoadScene("MultiplayerSelectionScene");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
