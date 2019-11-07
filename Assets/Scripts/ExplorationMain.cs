using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class ExplorationMain : MonoBehaviour {
    float lastSpawnedTime = 0;
    TileMap gameTiles;
    public Player agent;
    Vector2 oldPos = Vector2.zero;
    public int scanSize = 1;
    public bool exploring = true;

    void Start() {
        gameTiles = GetComponent<TileMap>();
        oldPos = transform.position.xz();
    }

    void Update() {
        Vector2 pos = getCurrentPos();

        if (pos != Vector2.Min(pos, gameTiles.size - Vector2.one * scanSize) || pos != Vector2.Max(pos, Vector2.one * scanSize)) {
            exploring = false;
        } else {
            exploring = true;
        }
        agent.explorer = this; //hacky
        var curPos = pos;
        if (curPos != oldPos || Time.frameCount < 5) {
            scanPos(curPos);
            oldPos = curPos;
        }
        //getCurrentTile().GetComponent<MeshRenderer>().enabled = false;
    }

    public Tile getCurrentTile() {
        return gameTiles.GetTile(getCurrentPos());
    }
    Vector2 getCurrentPos() {
        return (agent.transform.position - transform.position).xz();
    }
    void scanPos(Vector2 pos) {
        pos -= Vector2.one * .5f;
        //print("exploring");
        Vector2 ij;
        for (int i = -scanSize; i <= scanSize; i++) {
            for (int j = -scanSize; j <= scanSize; j++) {
                //print("scanning " + i + " " + j);
                ij = new Vector2(i, j);
                if (ij.magnitude - .1f < scanSize) {
                    gameTiles.explore(i + pos.x, j + pos.y, agent);
                }
            }
        }
    }
}
