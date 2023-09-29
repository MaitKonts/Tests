using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WorldSelectionController : MonoBehaviour
{
    public GameObject worldListPanel; // Assign in Inspector
    public Button addButton; // Assign in Inspector
    public Button deleteButton; // Assign in Inspector
    public Button backButton; // Declare the back button
    public WorldManager worldManager; // Add this line
    public Dropdown worldsDropdown;
    private List<string> worlds;
    
    void Start()
    {
        // Load the list of saved worlds
        LoadWorlds();
        
        // Set up button listeners
        addButton.onClick.AddListener(AddWorld);
        deleteButton.onClick.AddListener(DeleteWorld);
        backButton.onClick.AddListener(GoBackToMainMenu); // Add listener for back button
    }
    
    void LoadWorlds()
    {
        // Check if worldManager is not null before accessing its methods
        if (worldManager == null)
        {
            Debug.LogError("worldManager is not assigned in WorldSelectionController");
            return;
        }

        List<string> worlds = worldManager.GetSavedWorlds();

        // Check if worldsDropdown is not null before accessing its methods
        if (worldsDropdown == null)
        {
            Debug.LogError("worldsDropdown is not assigned in WorldSelectionController");
            return;
        }

        worldsDropdown.ClearOptions();
        worldsDropdown.AddOptions(worlds);
    }
    
    void AddWorld()
    {
        SceneManager.LoadScene("GameScene");    // Implement functionality to add a new world
    }
    
    void DeleteWorld()
    {
        // Implement functionality to delete the selected world
    }

    public void GoBackToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu"); // Replace with the actual name of your main menu scene
    }
    
    public void StartSelectedWorld()
    {
        // Use worldsDropdown to access the selected option
        string selectedWorld = worldsDropdown.options[worldsDropdown.value].text;
        
        if (!string.IsNullOrEmpty(selectedWorld))
        {
            SceneManager.LoadScene(selectedWorld);
        }
    }
}