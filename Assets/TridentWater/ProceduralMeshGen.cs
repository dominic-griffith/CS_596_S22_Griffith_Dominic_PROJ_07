using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMeshGen : MonoBehaviour {
    public float materialScale = 10f;

    public Vector2Int resolution; // the number of vertices in each direction
    public Vector2 scale; // the scale of the mesh

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private Mesh mesh;

    private void Awake() {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }
    
    
    

    // generates the mesh and attaches it to the gameObject
    public void generateMesh() {
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
        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
        // apply mesh
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}
