using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ProceduralMeshGen : MonoBehaviour {
    private const string MESH_SAVE_FILEPATH = "TridentWater/Mesh";
    
    public float materialScale = 10f;

    public Vector2Int resolution; // the number of vertices in each direction
    public Vector2 scale; // the scale of the mesh

    public bool generate = false;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    private void Update() {
        if (generate) {
            generate = false;
            saveMesh();
            print("Mesh saved successfully.");
        }
    }


    public void loadMesh() {
        Mesh mesh = Resources.Load<Mesh>(MESH_SAVE_FILEPATH);
        applyMesh(mesh);
    }

    // applies the mesh and attaches it to the gameObject
    private void applyMesh(Mesh mesh) {
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }


    private Mesh generateMesh() {
        Vector3[] vertices = new Vector3[(resolution.x + 1) * (resolution.y + 1)];
        Vector2[] uv = new Vector2[(resolution.x + 1) * (resolution.y + 1)];
        int[] triangles = new int[resolution.x * resolution.y * 6];

        // vertices and uv's
        for (int x = 0, i = 0; x <= resolution.x; x++) {
            for (int z = 0; z <= resolution.y; z++, i++) {
                vertices[i] = new Vector3(x * (scale.x / resolution.x), 0, z * (scale.y / resolution.y));
                uv[i] = new Vector2((float) x / resolution.x, (float) z / resolution.y);
            }
        }
        
        // triangles
        for (int x = 0, tPos = 0, i = 0; x < resolution.x; x++, i++) {
            for (int z = 0; z < resolution.y;  z++, tPos += 6, i++) {
                // note that winding order must be clockwise for normals to face upwards
                triangles[tPos] = i;
                triangles[tPos + 1] = i + 1;
                triangles[tPos + 2] = i + resolution.y + 1;
                triangles[tPos + 3] = i + 1;
                triangles[tPos + 4] = i + resolution.y + 2;
                triangles[tPos + 5] = i + resolution.y + 1;
            }
        }
        
        // setup mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        return mesh;
    }

    private void saveMesh() {
        #if UNITY_EDITOR
        Mesh mesh = generateMesh();
        AssetDatabase.CreateAsset(mesh, "Assets/Resources/" + MESH_SAVE_FILEPATH + ".asset");
        #endif
    }
}
