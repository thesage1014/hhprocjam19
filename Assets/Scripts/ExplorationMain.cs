using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMain : MonoBehaviour
{
    float lastSpawnedTime = 0;
    TileMap gameTiles;
    public Transform agentPos;

    void Start()
    {
        gameTiles = GetComponent<TileMap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnedTime >= 1) {
            gameTiles.getTile(0,(int)Time.time);
            lastSpawnedTime = Time.time;
        }
    }
    Tile getCurrentTile() {
        return gameTiles.getTile((int)agentPos.localPosition.x, (int)agentPos.localPosition.z);
    }
    Vector2 getCurrentPos() {
        return new Vector2((int)agentPos.localPosition.x, (int)agentPos.localPosition.z);
    }
}
