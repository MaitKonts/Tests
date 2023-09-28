using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject blockPrefab;
    public int chunkSize = 25;

    public void GenerateChunk()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                // Calculate block position within the chunk
                Vector3 blockPos = new Vector3(x, GenerateHeight(x + transform.position.x, z + transform.position.z), z);
                
                // Instantiate block
                Instantiate(blockPrefab, blockPos + transform.position, Quaternion.identity, transform);
            }
        }
    }

    float GenerateHeight(float x, float z)
    {
        // Generate a height value using Perlin noise (adjust scale and amplitude)
        float height = Mathf.PerlinNoise(x / 20f, z / 20f) * 10f;
        return height;
    }
    
    public string GetSaveData()
    {
        // Iterate through all blocks and gather their data
        // For simplicity, let’s say each block has an integer state.
        string saveData = "";
        foreach (Transform block in transform)
        {
            // Gather block data here...
            // For example:
            int blockState = 1; // Replace with actual block state
            saveData += blockState.ToString() + ",";
        }
        return saveData;
    }
}
