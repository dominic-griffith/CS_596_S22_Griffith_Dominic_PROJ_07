using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetailGen : MonoBehaviour {
    private const int TEST_RES = 100;
    private const int OBJECTS_TO_PLACE = 20000;
    
    public Transform testPrefab;

    private Terrain terrain;

    private void Awake() {
        terrain = GetComponent<Terrain>();
    }

    private void Start() {
        placeRandomObjects();
    }
    
    
    // places random objects
    private void placeRandomObjects() {
        for (int i = 0; i < OBJECTS_TO_PLACE; i++) {
            Vector3 relativePos = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
            Transform newObj = Instantiate(testPrefab, transform);
            placeObject(newObj, relativePos);
        }
    }
    
    
    // places objects in a grid to test placement system
    private void testPlacement() {
        for (int x = 0; x < TEST_RES; x++) {
            for (int z = 0; z < TEST_RES; z++) {
                Vector3 relativePos = new Vector3((float) x / TEST_RES, 0, (float) z / TEST_RES);
                Transform newObj = Instantiate(testPrefab, transform);
                placeObject(newObj, relativePos);
            }
        }
    }


    // places an object on the terrain (relative pos is scaled from 0 to 1)
    private void placeObject(Transform obj, Vector3 relativePos) {
        Vector3 terrainSize = terrain.terrainData.size;
        float resolution = terrain.terrainData.heightmapResolution;
        
        Vector3 objPos = terrain.transform.position + new Vector3(relativePos.x * terrainSize.x, 0, relativePos.z * terrainSize.z);
        objPos.y = terrain.terrainData.GetHeight(Mathf.FloorToInt(relativePos.x * resolution), Mathf.FloorToInt(relativePos.z * resolution));
        obj.transform.position = objPos;
    }
}
