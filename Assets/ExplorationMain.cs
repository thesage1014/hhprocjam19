using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMain : MonoBehaviour
{
    public List<Tile> tilePrefabs;
    float lastSpawnedTime = 0;
    var gameTiles; 

    void Start()
    {
        gameTiles = new Filemap();
    }

    // Update is called once per frame
    void Update()
    {
            
        if(Time.time - lastSpawnedTime >= 1) {
            spawnTile(0,(int)Time.time);
            lastSpawnedTime = Time.time;
        }
    }

    private void spawnTile(float x, float z)
    {
        Tile newtile = Instantiate<Tile>(tilePrefabs[Random.Range(0,tilePrefabs.Count)]);
        gameTiles.addTile(x, 0, newTile);
        newtile.transform.position = new Vector3(x, 0, z);
    }
}
