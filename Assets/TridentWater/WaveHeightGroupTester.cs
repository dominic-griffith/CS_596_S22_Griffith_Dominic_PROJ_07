using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHeightGroupTester : MonoBehaviour {
    private const float CUBE_SPAWN_FACTOR = 0.1f;
    
    public TridentController targetController;
    public Transform cubePrefab;
    public float mult;

    private ProceduralMeshGen meshGen;
    private List<Transform> testObjects = new List<Transform>();

    private void Awake() {
        meshGen = targetController.GetComponent<ProceduralMeshGen>();
        for (int x = 0, i = 0; x < meshGen.resolution.x * CUBE_SPAWN_FACTOR; x++) {
            for (int y = 0; y < meshGen.resolution.y * CUBE_SPAWN_FACTOR; y++, i++) {
                Transform testObj = Instantiate(cubePrefab, transform);
                testObj.position = targetController.transform.position + new Vector3(((float)(x / CUBE_SPAWN_FACTOR)  / meshGen.resolution.x) * meshGen.scale.x, 0, ((float)(y / CUBE_SPAWN_FACTOR) / meshGen.resolution.y) * meshGen.scale.y);
                testObjects.Add(testObj);
            }
        }
    }

    private void Update() {
        foreach (Transform testObj in testObjects) {
            testObj.position = new Vector3(testObj.position.x, targetController.transform.position.y, testObj.position.z);
            testObj.position += new Vector3(0, targetController.getWaveHeight(testObj.position) * mult, 0);
        }
    }
}
