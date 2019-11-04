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
    public bool exploring = true;

    void Start()
    {
        gameTiles = GetComponent<TileMap>();
    }
    
    void Update()
    {
        if(exploring) {
            var curPos = getCurrentPos();
            if (!curPos.Equals(oldPos)) {
                explore(curPos);
                oldPos = curPos;
            }
        }
    }

    Tile getCurrentTile() {
        return gameTiles.getTile(Mathf.RoundToInt(agentPos.localPosition.x), Mathf.RoundToInt(agentPos.localPosition.z));
    }
    Vector2 getCurrentPos() {

        return new Vector2(agentPos.localPosition.x, (int)agentPos.localPosition.z);
    }
    void explore(Vector2 pos) {
        print("exploring");
        for(int i=-scanSize; i<=scanSize; i++) {
            for (int j = -scanSize; j <= scanSize; j++) {
                print("scanning " + i + " " + j);
                gameTiles.getTile(i+(int)pos.x, j+(int)pos.y);
            }
        }
    }
}
