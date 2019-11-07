using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

[System.Serializable]
public class PredefinedTile {
    public Vector2Int pos;
    public bool prebuilit;
    public Tile prefab;
}

public class TileMap : MonoBehaviour {
    [SerializeField] List<Tile> inputTilePrefabs;
    [SerializeField] List<PredefinedTile> predefinedTiles;
    Dictionary<string, Tile> tilePrefabs;
    public Tile defaultGround;

    int xCells = 40;
    int yCells = 40;
    Tile[,] tileMap;
    Tile lastRespawnableTile;

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

    void Awake() {
        tilePrefabs = new Dictionary<string, Tile>();
        foreach (Tile t in inputTilePrefabs) {
            t.tileType = t.name;
            tilePrefabs.Add(t.name, t);
        }
    }
    void Start() {
        tileMap = new Tile[xCells, yCells];
        List<Tile> existingTiles = new List<Tile>(GetComponentsInChildren<Tile>());
        foreach (Tile t in existingTiles) {
            mapTile(t);
        }
        foreach (PredefinedTile t in predefinedTiles) {
            if (t.prefab != null) {
                setTile(t.pos.x, t.pos.y, t.prefab, t.prebuilit);
            }
        }
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
        int x = (int)inx;// Mathf.RoundToInt(inx);
        int y = (int)iny;// Mathf.RoundToInt(iny);
        if (x >= xCells || x < 0 || y >= yCells || y < 0) {
            Debug.Log("Tile search out of bounds:" + x + " " + y);
            return null;
        } else {
            Tile returnTile = tileMap[x, y];
            if (returnTile is null) { // Empty space
                generateTile(x, y, agent);
            } else {
                if (!(agent is null)) {
                    returnTile.beExplored(returnTile.transform.position.xz() - agent.transform.position.xz());
                }
            }
            return returnTile;
        }
    }
    // Populate new tile. Agent can be null
    Tile generateTile(int x, int y, Player agent) {
        //print(x + " " + y);
        Tile newTile = null;
        if (agent is null) {// || Time.frameCount < 10) {
            //if ((int)Random.Range(0, 10) == 0) {
            //    newTile = Instantiate<Tile>(tilePrefabs["wall"], transform);
            //} else {
            //    newTile = Instantiate<Tile>(tilePrefabs["ground"], transform);
            //}
        } else {
            int scanSize = 1;
            Tile agentTile = agent.explorer.getCurrentTile();
            Tile tileToSpawn = null;
            if (agentTile is null) {
                tileToSpawn = defaultGround;
            } else {
                if (tilePrefabs.ContainsKey(agentTile.tileType) && agentTile.respawnable) {
                    tileToSpawn = tilePrefabs[agentTile.tileType];
                    lastRespawnableTile = tileToSpawn;
                } else {
                    //Debug.Log("prefab does not exist?");
                    if (lastRespawnableTile is null) {
                        tileToSpawn = defaultGround;
                    } else {
                        tileToSpawn = lastRespawnableTile;
                    }
                }
                for (int i = -scanSize; i <= scanSize; i++) {
                    for (int j = -scanSize; j <= scanSize; j++) {
                        //print("scanning " + i + " " + j);
                        Tile scannedTile = GetTile(i + x, y); // j +
                        //print(i + " " + j);
                        if (!(scannedTile is null) && scannedTile.tileType == "wall") {
                            tileToSpawn = tilePrefabs["wall"];
                            break;
                        }
                    }
                }
            }
            newTile = Instantiate<Tile>(tileToSpawn, transform);
            //newTile = Instantiate<Tile>(agent.explorer.getCurrentTile(), transform);
        }
        newTile.transform.position = new Vector3(x + .5f, 0, y + .5f) + transform.position;
        newTile.animatedSpawn = true;
        tileMap[x, y] = newTile;
        return newTile;
    }
    Tile setTile(int x, int y, Tile tile, bool prebuilt) {
        if (tileMap[x, y] is null) {

            Tile newTile = Instantiate<Tile>(tile, transform);
            newTile.transform.position = new Vector3(x + .5f, 0, y + .5f) + transform.position;
            tileMap[x, y] = newTile;
            if (prebuilt) {
                newTile.scaleSpeed = .8f;
            }
            return newTile;
        } else {
            return tileMap[x, y];
        }
    }
    void mapTile(Tile tile) {
        Vector2Int pos = roundedPos(tile);
        if (!inBounds(pos.x, pos.y)) {
            Debug.LogError("out of bounds tile " + tile);
        } else {
            if (tileMap[pos.x, pos.y] is null) {
                tile.transform.position = new Vector3(pos.x + .5f, 0, pos.y + .5f) + transform.position;
                tileMap[pos.x, pos.y] = tile;
            } else {
                Debug.LogError("position not null " + tile);
            }
        }
    }
    Vector2Int roundedPos(Tile tile) {
        return Vector3Int.RoundToInt(tile.transform.position - transform.position).xz();
    }
    public bool inBounds(int x, int y) {
        return !(x >= xCells || x < 0 || y >= yCells || y < 0);
    }
}