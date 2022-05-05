using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentController : MonoBehaviour {
    [Header("Tiling")]
    public Transform tilePrefab;
    public Vector2Int tileCount;
    
    [Header("Wave Parameters")]
    public Vector4 speeds;
    public Vector4 scales;
    public float perlinNoiseScale;
    public float masterIntensity;
    public Vector4 intensities;
    public float perlinNoiseIntensity;

    private List<Transform> tiles;
    private ProceduralMeshGen meshGen;
    private Material material;
    private float waveTime = 0;


    private void Awake() {
        meshGen = tilePrefab.GetComponent<ProceduralMeshGen>();
        material = tilePrefab.GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Start() {
        for (int x = 0; x < tileCount.x; x++) {
            for (int z = 0; z < tileCount.y; z++) {
                Transform newTile = Instantiate(tilePrefab, transform);
                newTile.transform.localPosition = new Vector3(x * meshGen.scale.x, 0, z * meshGen.scale.y);
                newTile.GetComponent<ProceduralMeshGen>().loadMesh();
            }
        }
    }

    private void Update() {
        // update time
        waveTime = Time.timeSinceLevelLoad * 0.1f;
        
        // send all wave parameters to shader
        material.SetFloat("_WaveMasterIntensity", masterIntensity);
        material.SetVector("_WorleySpeeds", speeds);
        material.SetVector("_WorleyScales", scales);
        material.SetFloat("_PerlinNoiseScale", perlinNoiseScale);
        material.SetVector("_WorleyIntensities", intensities);
        material.SetFloat("_PerlinNoiseIntensity", perlinNoiseIntensity);
        material.SetFloat("_WaveTime", waveTime);
    }
    
    
    
    // samples wave height at a single point (do not use this en masse)
    public float getWaveHeight(Vector3 worldPos) {
        // determine position within the water plane
        Vector2 localPos = new Vector2(worldPos.x - transform.position.x, worldPos.z - transform.position.z);
        Vector2 scaledPos = localPos / meshGen.scale;
        
        // find the wave height from each channel of the wave texture
        float sampleTotal = 0;
        float intensityTotal = 0;
        for (int i = 0; i < 4; i++) {
            Vector2 speedOffset = speeds[i] * waveTime * new Vector2(1, 1);
            sampleTotal += sampleChannel(i, (scaledPos + speedOffset) * scales[i]) * intensities[i];
            intensityTotal += intensities[i];
        }
        return transform.position.y + ((sampleTotal / intensityTotal) * masterIntensity);

        
        // get the value of a specified color channel in the wave texture at pos, a vector2 between 0 and 1
        float sampleChannel(int channel, Vector2 pos) {
            Texture2D tex = material.GetTexture("_WorleyNoiseA") as Texture2D;
            Vector2 pixelPos = new Vector2(pos.x * tex.width, pos.y * tex.height);
            Color color = tex.GetPixel((int)pixelPos.x, (int)pixelPos.y);
            return color[channel];
        }
    }
}
