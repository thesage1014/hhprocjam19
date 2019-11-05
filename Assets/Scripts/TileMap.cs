using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class TileMap : MonoBehaviour {
    public List<Tile> tilePrefabs;
    int xCells = 40;
    int yCells = 40;
    Tile[,] tileMap;
    //TODO add enum
    public Vector2 size {
        get {
            return new Vector2(xCells, yCells);
        }
    }
    public Vector3 size3 {
        get {
            return new Vector3(xCells, 0, yCells);
        }
    }
    

    // Start is called before the first frame update
    void Start() {
        tileMap = new Tile[xCells, yCells];
        Transform helper = transform.Find("MapHelper");
        helper.transform.localScale = new Vector3(xCells, .1f, yCells);
        helper.transform.position = transform.position + new Vector3(xCells / 2, -.05f, yCells / 2);
    }

    // Update is called once per frame
    void Update() {

    }
    public Tile getStartingTile() {
        return explore(xCells / 2, yCells / 2, null);
    }
    public Tile GetTile(int x, int y) {
        if (x >= xCells || x < 0 || y >= yCells || y < 0) {
            //Debug.Log("Tile seach out of bounds:" + x + " " + y);
            return null;
        } else {
            Tile returnTile = tileMap[x, y];
            //might be null!
            return returnTile;
        }
    }
    public Tile GetTile(float x, float y) {
        return GetTile((int)x, (int)y);
    }
    public Tile GetTile(Vector2 inPos) {
        return GetTile((int)inPos.x, (int)inPos.y);
    }
    public Tile explore(float inx, float iny, Player agent) { // agent can be null
        int x = Mathf.RoundToInt(inx);
        int y = Mathf.RoundToInt(iny);
        if (x >= xCells || x < 0 || y >= yCells || y < 0) {
            Debug.Log("Tile seach out of bounds:" + x + " " + y);
            return null;
        } else {
            Tile returnTile = tileMap[x, y];
            if (returnTile is null) {
                addTile(x, y, agent);
            } else {
                if (!(agent is null)) {
                    returnTile.explore(returnTile.transform.position.xz() - agent.transform.position.xz());
                }
            }
            return returnTile;
        }
    }
    // Populate new tile
    Tile addTile(int x, int y, Player agent) {
        //print(x + " " + y);
        Tile newTile = null;
        if (agent is null || Time.frameCount < 10) {
            newTile = Instantiate<Tile>(tilePrefabs[Random.Range(0, tilePrefabs.Count)], transform);
        } else {
            int scanSize = 1;
            Tile tileToSpawn = agent.explorer.getCurrentTile();
            for (int i = -scanSize; i <= scanSize; i++) {
                for (int j = -scanSize; j <= scanSize; j++) {
                    //print("scanning " + i + " " + j);
                    Tile scannedTile = GetTile(i + x, j + y);
                    print(i + " " + j);
                    if (!(scannedTile is null) && scannedTile.tag == "wall") {
                        tileToSpawn = scannedTile;
                        break;
                    }
                }
            }
            newTile = Instantiate<Tile>(tileToSpawn, transform);
            //newTile = Instantiate<Tile>(agent.explorer.getCurrentTile(), transform);
        }
        newTile.transform.position = new Vector3(x + .5f, 0, y + .5f) + transform.position;
        tileMap[x, y] = newTile;
        return newTile;
    }
}