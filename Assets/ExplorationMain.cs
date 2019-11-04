using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMain : MonoBehaviour
{
    public List<Tile> tilePrefabs;
    float lastSpawnedTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastSpawnedTime >= 1)
        {
            spawnTile(0,(int)Time.time);
        }
        lastSpawnedTime = Time.time;
    }

    private void spawnTile(float x, float z)
    {
        
    }
}
