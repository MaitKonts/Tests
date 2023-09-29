using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject treePrefab; 
    public GameObject pigPrefab;
    public int chunkSize = 25;
    public float terrainScale = 20f;
    public float terrainHeight = 10f;

    private void Start()
    {
        GenerateChunk();
    }

    public void GenerateChunk()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(chunkSize + 1) * (chunkSize + 1)];
        int[] triangles = new int[chunkSize * chunkSize * 6];

        int vertIndex = 0;
        int triIndex = 0;

        // 75% chance to skip tree spawning for this chunk
        bool spawnTree = Random.value >= 0.75f;
        int treeX = Random.Range(0, chunkSize);
        int treeZ = Random.Range(0, chunkSize);

        bool spawnPig = Random.value <= 0.02f;
        int pigX = Random.Range(0, chunkSize);
        int pigZ = Random.Range(0, chunkSize);

        for (int x = 0; x <= chunkSize; x++)
        {
            for (int z = 0; z <= chunkSize; z++)
            {
                float height = Mathf.PerlinNoise((x + transform.position.x) / terrainScale, (z + transform.position.z) / terrainScale) * terrainHeight;
                vertices[vertIndex] = new Vector3(x, height, z);

                // Spawn a single tree at a random position within the chunk
                if (spawnTree && x == treeX && z == treeZ)
                {
                    Vector3 treePosition = new Vector3(x, height, z) + transform.position;
                    Instantiate(treePrefab, treePosition, Quaternion.identity, transform);
                }

                if (spawnPig && x == pigX && z == pigZ)
                {
                    Vector3 pigPosition = new Vector3(x, height, z) + transform.position;
                    Instantiate(pigPrefab, pigPosition, Quaternion.identity, transform);
                }


                if (x < chunkSize && z < chunkSize)
                {
                    triangles[triIndex] = vertIndex;
                    triangles[triIndex + 1] = vertIndex + 1;
                    triangles[triIndex + 2] = vertIndex + chunkSize + 1;
                    triangles[triIndex + 3] = vertIndex + 1;
                    triangles[triIndex + 4] = vertIndex + chunkSize + 2;
                    triangles[triIndex + 5] = vertIndex + chunkSize + 1;
                    triIndex += 6;
                }

                vertIndex++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
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
    public void LoadChunk(string chunkData)
    {
        // Parse the chunkData string and apply it to load the chunk
        // This will depend on how the data was saved in the GetSaveData method
        string[] blockStates = chunkData.Split(',');
        int index = 0;
        foreach (Transform block in transform)
        {
            // Apply block state from saved data
            // For example:
            int blockState = int.Parse(blockStates[index]);
            // Apply the blockState to the block here...
            
            index++;
        }
    }
}
