using UnityEditor;                                  // Editor need removed class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]                                 // Need to run in EditMode

public class PGCTerrain : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;

    public Vector3 heightMapScale = new Vector3(1, 1, 1);
    public float hillVariance = 20f;
    public float hillVariance2 = 100f;
    public Texture2D heightMapImage;
    public Vector2 offset = new Vector2(2f, 2f);

    private void OnEnable()                         // Like Awake but for editor
    {
        terrain = this.GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }

    public void PerlinMap()
    {
        float[,] heightMap;
        float[,] multiplier;
        float[,] multiplier2;
        heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        multiplier = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        multiplier2 = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int x = 0; x < terrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < terrainData.heightmapResolution; z++)
            {
                multiplier[x, z] = Mathf.PerlinNoise((offset.x + (float)x / (float)terrainData.heightmapResolution) * hillVariance, offset.y + ((float)z / (float)terrainData.heightmapResolution) * hillVariance) / 10f;
                multiplier2[x, z] = Mathf.PerlinNoise((offset.x + (float)x / (float)terrainData.heightmapResolution) * hillVariance2, offset.y + ((float)z / (float)terrainData.heightmapResolution) * hillVariance2) / 10f;
                heightMap[x, z] = heightMapImage.GetPixel((int)(x * heightMapScale.x), (int)(z * heightMapScale.z)).grayscale * heightMapScale.y + multiplier[x, z] + multiplier2[x, z];
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }
}
