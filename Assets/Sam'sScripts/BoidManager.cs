using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {

    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;
    public int type;
    List<Boid> boids; 
    Boid[] allBoids;

    void Start () {
        boids = new List<Boid>();
        allBoids = FindObjectsOfType<Boid> ();
        foreach (Boid b in allBoids)
        {
            if (b.boidType == type)
            {
                boids.Add(b);
                b.Initialize(settings, null);
            }  
        }
    }

    void Update () {
        if (boids != null) {

            int numBoids = boids.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Count; i++) {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].forward;
                boidData[i].boidType = boids[i].boidType;
            }

            var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
            boidBuffer.SetData (boidData);

            compute.SetBuffer (0, "boids", boidBuffer);
            compute.SetInt ("numBoids", boids.Count);
            compute.SetFloat ("viewRadius", settings.perceptionRadius);
            compute.SetFloat ("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt (numBoids / (float) threadGroupSize);
            compute.Dispatch (0, threadGroups, 1, 1);

            boidBuffer.GetData (boidData);

            for (int i = 0; i < boids.Count; i++) {
                boids[i].avgFlockHeading = boidData[i].flockHeading;
                boids[i].centreOfFlockmates = boidData[i].flockCentre;
                boids[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                boids[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                boids[i].UpdateBoid ();
            }

            boidBuffer.Dispose ();
        }
    }

    public struct BoidData {
        public Vector3 position;
        public Vector3 direction;
        public int boidType;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size {
            get {
                return sizeof (float) * 3 * 5 + sizeof (int) + sizeof(int);
            }
        }
    }
}