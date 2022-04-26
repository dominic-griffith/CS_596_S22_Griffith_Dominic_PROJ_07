using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public enum GizmoType { Never, SelectedOnly, Always }

    public Boid prefab;
    public int numGroups;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public GizmoType showSpawnRegion;

    void Awake () {
        for (int i = 0; i < spawnCount; i++)
        {
            int groupNum = (int)Mathf.Floor(Random.Range(0f, 3f));
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate (prefab);
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;

            if(groupNum == 0)
            {
                boid.SetColour(Color.red);
                boid.SetType(0);
            }
                
            else if (groupNum == 1)
            {
                boid.SetColour(Color.blue);
                boid.SetType(1);
            }
                
            else if (groupNum == 2)
            {
                boid.SetColour(Color.green);
                boid.SetType(2);
            }
        }
    }

    private void OnDrawGizmos () {
        if (showSpawnRegion == GizmoType.Always) {
            DrawGizmos ();
        }
    }

    void OnDrawGizmosSelected () {
        if (showSpawnRegion == GizmoType.SelectedOnly) {
            DrawGizmos ();
        }
    }

    void DrawGizmos () {

        Gizmos.DrawSphere (transform.position, spawnRadius);
    }

}