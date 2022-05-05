using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawner : MonoBehaviour
{
    public List<BoidSupervisor> supervisors;
    public int spawnDist = 50;

    private Transform player;

    private Terrain terry;
    private TridentController water;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        terry = GameObject.Find("Terrain").GetComponent<Terrain>();
        water = GameObject.Find("TridentWater").GetComponent<TridentController>();

        Spawn();
    }

    void Spawn()
    {
        Vector3 pos = Vector3.zero;

        float waterHeight;
        Vector3 terryRelPos;
        float terrainHeight;

        int count = 0;

        bool foundValid = false;

        BoidSupervisor sup;

        for(int i = 0; i < supervisors.Count; i ++)
        {
            while(!foundValid && count < 16)
            {
                pos = new Vector3(Random.Range(-spawnDist, spawnDist), 0, Random.Range(spawnDist, spawnDist)) + player.position;

                waterHeight = water.getWaveHeight(pos);
                terryRelPos = ((pos - terry.transform.position) / terry.terrainData.size.x) * terry.terrainData.heightmapResolution;
                terrainHeight = terry.terrainData.GetHeight((int)terryRelPos.x, (int)terryRelPos.z);

                //check if we are in the water and there is enougph space for a spawner
                if (waterHeight - terrainHeight > 7)
                {
                    pos.y = Random.Range(terrainHeight, waterHeight);
                    foundValid = true;
                }
                count++;
            }

            foundValid = false;
            count = 0;
            sup = GameObject.Instantiate(supervisors[i]);
            sup.transform.position = pos;
        }
    }
}
