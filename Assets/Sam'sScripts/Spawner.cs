﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public enum GizmoType { Never, SelectedOnly, Always }

    public GameObject prefab;
    public float spawnRadius = 5;
    public int spawnCount = 10;
    public GizmoType showSpawnRegion;
    public int type;

    void Start () {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate(prefab).GetComponent<Boid>();
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;

            boid.SetType(type);
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