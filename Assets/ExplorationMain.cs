﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMain : MonoBehaviour
{
    public List<Tile> tilePrefabs;
    float lastSpawnedTime = 0;
    TileMap gameTiles; 

    void Start()
    {
        gameTiles = GetComponent<TileMap>();
    }

    // Update is called once per frame
    void Update()
    {
            
        if(Time.time - lastSpawnedTime >= 1) {
            print(Time.frameCount);
            spawnTile(0,(int)Time.time);
            lastSpawnedTime = Time.time;
            print(gameTiles.getStartingTile());
        }
    }

    private void spawnTile(int x, int z)
    {
        Tile newtile = Instantiate<Tile>(tilePrefabs[Random.Range(0,tilePrefabs.Count)]);
        newtile.transform.position = new Vector3(x, 0, z);
        gameTiles.addTile(x, z, newtile);
    }
}
