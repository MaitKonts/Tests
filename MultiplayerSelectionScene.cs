using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerSelectionScene : MonoBehaviour
{
    public Button backButton; // Declare the back button
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(GoBackToMainMenu); // Add listener for back button
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu"); // Replace with the actual name of your main menu scene
    }
}
