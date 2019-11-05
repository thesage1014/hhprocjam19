using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class ExplorationMain : MonoBehaviour
{
    float lastSpawnedTime = 0;
    TileMap gameTiles;
    public Player agent;
    Vector2 oldPos = Vector2.zero;
    public int scanSize = 1;
    public bool exploring = true;

    void Start()
    {
        gameTiles = GetComponent<TileMap>();
        oldPos = transform.position.xz();
    }
    
    void Update()
    {
        Vector2 posDiff = (agent.transform.position - transform.position).xz();
        
        if(posDiff != Vector2.Min(posDiff,gameTiles.size-Vector2.one*scanSize) || posDiff != Vector2.Max(posDiff, Vector2.one * scanSize)) {
            exploring = false;
        } else {
            exploring = true;
            agent.explorer = this; //hacky
            var curPos = getCurrentPos();
            if (curPos != oldPos) {
                explore(curPos);
                oldPos = curPos;
            }
        }
    }

    Tile getCurrentTile() {
        return gameTiles.GetTile(agent.transform.localPosition.x,agent.transform.localPosition.z);
    }
    Vector2 getCurrentPos() {
        return (agent.transform.position-transform.position).xz();
    }
    void explore(Vector2 pos) {
        //print("exploring");
        for(int i=-scanSize; i<=scanSize; i++) {
            for (int j = -scanSize; j <= scanSize; j++) {
                //print("scanning " + i + " " + j);
                gameTiles.explore(i+pos.x, j+pos.y, pos);
            }
        }
    }
}
