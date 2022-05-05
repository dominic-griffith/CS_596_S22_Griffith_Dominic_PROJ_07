using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetailGen : MonoBehaviour {
    private const int TEST_RES = 100;

    public bool doRandomGen = true;
    public int objectsToPlace = 20000;

    private Terrain terrain;
    private TridentController tridentWater;
    private Transform[] oceanPrefabs;

    private float relativeWaterLevel;

    private void Awake() {
        terrain = GetComponent<Terrain>();
        tridentWater = GameObject.Find("TridentWater").GetComponent<TridentController>();
        oceanPrefabs = Resources.LoadAll<Transform>("Prefabs/TerrainGen/Ocean");

        relativeWaterLevel = getRelativeWaterLevel();
    }

    private void Start() {
        if (doRandomGen) placeRandomObjects();
    }


    private float getRelativeWaterLevel() {
        Vector3 terrainSize = terrain.terrainData.size;
        return (tridentWater.transform.position.y - transform.position.y) / terrainSize.y;
    }
    
    
    // places random objects
    private void placeRandomObjects() {
        for (int i = 0; i < objectsToPlace; i++) {
            Vector3 relativePos = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
            createAndPlaceObject(oceanPrefabs[Random.Range(0, oceanPrefabs.Length)], relativePos, 0, relativeWaterLevel);
        }
    }
    
    
    // places objects in a grid to test placement system
    private void testPlacement() {
        for (int x = 0; x < TEST_RES; x++) {
            for (int z = 0; z < TEST_RES; z++) {
                Vector3 relativePos = new Vector3((float) x / TEST_RES, 0, (float) z / TEST_RES);
                createAndPlaceObject(oceanPrefabs[Random.Range(0, oceanPrefabs.Length)], relativePos);
            }
        }
    }


    // places an object on the terrain (relative pos is scaled from 0 to 1)
    private void createAndPlaceObject(Transform objPrefab, Vector3 relativePos, float minHeight = 0f, float maxHeight = 1f) {
        Vector3 terrainSize = terrain.terrainData.size;
        float resolution = terrain.terrainData.heightmapResolution;
        
        float terrainHeight = terrain.terrainData.GetHeight(Mathf.FloorToInt(relativePos.x * resolution), Mathf.FloorToInt(relativePos.z * resolution));
        float relativeTerrainHeight = terrainHeight / terrainSize.y;
        if (relativeTerrainHeight >= minHeight && relativeTerrainHeight <= maxHeight) {
            Vector3 objPos = terrain.transform.position + new Vector3(relativePos.x * terrainSize.x, 0, relativePos.z * terrainSize.z);
            objPos.y = terrainHeight;
            Transform obj = Instantiate(objPrefab, transform);
            obj.transform.position = objPos;
        }
    }
}
