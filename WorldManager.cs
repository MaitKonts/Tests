using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject chunkPrefab;
    public GameObject blockPrefab;
    public GameObject playerPrefab;
    public Transform player;
    public int renderDistance = 1;
    public int chunkSize = 25;
    
    private Dictionary<Vector2, Chunk> chunkDictionary = new Dictionary<Vector2, Chunk>();
    public string currentWorldName;
    void Start()
    {
        // Instantiate the Player at the start of the game
        Vector3 playerStartPosition = new Vector3(0, GeneratePlayerStartHeight(), 0);
        player = Instantiate(playerPrefab, playerStartPosition, Quaternion.identity).transform;
    }

    void Update()
    {
        LoadChunksAroundPlayer();
    }

    float GeneratePlayerStartHeight()
    {
        // Optionally, implement logic to calculate a suitable start height for the player.
        // For simplicity, returning 10f (assuming units are in meters).
        return 10f;
    }

    void LoadChunksAroundPlayer()
    {
        Vector2 currentChunkCoord = new Vector2(Mathf.FloorToInt(player.position.x / chunkSize), Mathf.FloorToInt(player.position.z / chunkSize));

        for (int xOffset = -renderDistance; xOffset <= renderDistance; xOffset++)
        {
            for (int zOffset = -renderDistance; zOffset <= renderDistance; zOffset++)
            {
                Vector2 viewedChunkCoord = currentChunkCoord + new Vector2(xOffset, zOffset);

                if (!chunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    // Chunk not loaded, load it
                    Vector3 chunkPosition = new Vector3(viewedChunkCoord.x * chunkSize, 0, viewedChunkCoord.y * chunkSize);
                    GameObject chunkObject = Instantiate(chunkPrefab, chunkPosition, Quaternion.identity);
                    Chunk chunk = chunkObject.AddComponent<Chunk>();
                    chunk.chunkSize = chunkSize;
                    chunk.GenerateChunk();

                    chunkDictionary.Add(viewedChunkCoord, chunk);
                }
            }
        }
    }

    public void SaveWorld()
    {
        // Check if a world name is set
        if (string.IsNullOrEmpty(currentWorldName)) return;

        // Save Player Position
        PlayerPrefs.SetFloat(currentWorldName + "_PlayerPosX", player.position.x);
        PlayerPrefs.SetFloat(currentWorldName + "_PlayerPosY", player.position.y);
        PlayerPrefs.SetFloat(currentWorldName + "_PlayerPosZ", player.position.z);

        // Save Chunks
        foreach (var chunkCoord in chunkDictionary.Keys)
        {
            Chunk chunk = chunkDictionary[chunkCoord];
            string chunkData = chunk.GetSaveData();
            PlayerPrefs.SetString(currentWorldName + "_Chunk_" + chunkCoord.x + "_" + chunkCoord.y, chunkData);
        }

        // Save the current world name to the list of saved worlds
        string savedWorlds = PlayerPrefs.GetString("SavedWorlds", "");
        List<string> worldsList = new List<string>(savedWorlds.Split(','));
        if (!worldsList.Contains(currentWorldName))
        {
            worldsList.Add(currentWorldName);
            PlayerPrefs.SetString("SavedWorlds", string.Join(",", worldsList));
        }

    }

    public void LoadWorld()
    {
        // Load the current world name
        currentWorldName = PlayerPrefs.GetString("CurrentWorld");

        // Check if a world name is loaded
        if (string.IsNullOrEmpty(currentWorldName)) return;

        // Load Player Position
        float x = PlayerPrefs.GetFloat(currentWorldName + "_PlayerPosX");
        float y = PlayerPrefs.GetFloat(currentWorldName + "_PlayerPosY");
        float z = PlayerPrefs.GetFloat(currentWorldName + "_PlayerPosZ");
        player.position = new Vector3(x, y, z);

        // Load Chunks
        foreach (var chunkCoord in chunkDictionary.Keys)
        {
            string chunkDataKey = currentWorldName + "_Chunk_" + chunkCoord.x + "_" + chunkCoord.y;
            if (PlayerPrefs.HasKey(chunkDataKey))
            {
                string chunkData = PlayerPrefs.GetString(chunkDataKey);
                Chunk chunk = chunkDictionary[chunkCoord];
                chunk.LoadChunk(chunkData);
            }
        }
    }

    public List<string> GetSavedWorlds()
    {
        string savedWorlds = PlayerPrefs.GetString("SavedWorlds", "");
        List<string> worldsList = new List<string>(savedWorlds.Split(','));
        worldsList.RemoveAll(string.IsNullOrEmpty); // Remove any empty strings
        return worldsList;
    }
}