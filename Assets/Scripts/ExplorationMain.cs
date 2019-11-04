using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationMain : MonoBehaviour
{
    float lastSpawnedTime = 0;
    TileMap gameTiles;
    public Transform agentPos;
    Vector2 oldPos = Vector2.zero;
    public int scanSize = 1;

    void Start()
    {
        gameTiles = GetComponent<TileMap>();
    }

    // Update is called once per frame
    void Update()
    {
        var curPos = getCurrentPos();
        if (!curPos.Equals(oldPos)) {
            explore(curPos);
            oldPos = curPos;
        }
        //if(Time.time - lastSpawnedTime >= 1) {
        //    gameTiles.getTile(0,(int)Time.time);
        //    lastSpawnedTime = Time.time;
        //}
    }
    Tile getCurrentTile() {
        return gameTiles.getTile((int)agentPos.localPosition.x, (int)agentPos.localPosition.z);
    }
    Vector2 getCurrentPos() {

        return new Vector2((int)agentPos.localPosition.x, (int)agentPos.localPosition.z);
    }
    void explore(Vector2 pos) {
        print("exploring");
        for(int i=-scanSize; i<scanSize; i++) {
            for (int j = -scanSize; j < scanSize; j++) {
                print("scanning " + i + " " + j);
                gameTiles.getTile(i+(int)pos.x, j+(int)pos.y);
            }
        }
    }
}
