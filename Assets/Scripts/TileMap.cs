using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class TileMap : MonoBehaviour {
    public List<Tile> tilePrefabs;
    int xCells = 40;
    int yCells = 40;
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
    Tile[,] tileMap;

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
        return explore(xCells / 2, yCells / 2);
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
    public Tile exploreTileDBG(int x, int y) {
        if (x >= xCells || x < 0 || y >= yCells || y < 0) {
            Debug.Log("Tile seach out of bounds:" + x + " " + y);
            return null;
        } else {
            Tile returnTile = tileMap[x, y];
            if (returnTile is null) {
                addTile(x, y);
            }
            return returnTile;
        }
    }
    public Tile explore(float inx, float iny, Vector2 agent) {
        int x = Mathf.RoundToInt(inx);
        int y = Mathf.RoundToInt(iny);
        if (x >= xCells || x < 0 || y >= yCells || y < 0) {
            Debug.Log("Tile seach out of bounds:" + x + " " + y);
            return null;
        } else {
            Tile returnTile = tileMap[x, y];
            if (returnTile is null) {
                addTile(x, y);
            } else {
                returnTile.explore(returnTile.transform.localPosition.xz() - agent);
            }
            return returnTile;
        }
    }
    public Tile explore(float inx, float iny) {
        return explore(inx, iny, new Vector2(inx, iny));
    }
    // Populate new tile
    Tile addTile(int x, int y) {
        //print(x + " " + y);
        Tile newTile = Instantiate<Tile>(tilePrefabs[Random.Range(0, tilePrefabs.Count)], transform);
        newTile.transform.position = new Vector3(x + .5f, 0, y + .5f) + transform.position;
        tileMap[x, y] = newTile;
        return newTile;
    }
}