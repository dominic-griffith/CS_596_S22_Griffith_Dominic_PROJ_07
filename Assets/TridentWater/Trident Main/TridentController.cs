using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TridentController : MonoBehaviour {
    public Vector4 speeds;
    public Vector4 scales;
    public float masterIntensity;
    public Vector4 intensities;

    private Material material;


    private void Awake() {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update() {
        material.SetFloat("_WaveMasterIntensity", masterIntensity);
        material.SetVector("_WorleySpeeds", speeds);
        material.SetVector("_WorleyScales", scales);
        material.SetVector("_WorleyIntensities", intensities);
    }
}
