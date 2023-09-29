using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private bool isPaused;
    public WorldManager worldManager;
    public InputField worldNameInputField;


    void Start()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false); // Ensure the pause menu is hidden at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void Resume()
    {
        TogglePauseMenu();
    }

    public void Quit()
    {
        // Add functionality to quit the game or return to the main menu
        Debug.Log("Quit!");
        Application.Quit();
    }
    public void SaveWorld()
    {
        string worldName = worldNameInputField.text;
        if (string.IsNullOrEmpty(worldName)) return;

        worldManager.currentWorldName = worldName;
        worldManager.SaveWorld();
    }

    public void LoadWorld()
    {
        string worldName = worldNameInputField.text;
        if (string.IsNullOrEmpty(worldName)) return;

        worldManager.currentWorldName = worldName;
        worldManager.LoadWorld();
    }
}
