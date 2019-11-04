using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {
    public List<Tile> tilePrefabs;
    static int xCells = 40;
    static int yCells = 40;
    Tile[,] tileMap;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new Tile[xCells, yCells];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Tile getStartingTile()
    {
        return getTile(xCells / 2, yCells / 2);
    }
    public Tile getTile(int x, int y)
    {
        if (x >= xCells || x < 0 || y >= yCells || y < 0)
        {
            Debug.LogError("Tile seach out of bounds:" + x + " " + y);
            return null;
        }
        else
        {
            Tile returnTile = tileMap[x, y];
            if(returnTile is null) {
                addTile(x, y);
            }
            return returnTile;
        }
    }

    // Populate new tile
    Tile addTile(int x, int y)
    {
        print(x + " " + y);
        Tile newTile = Instantiate<Tile>(tilePrefabs[Random.Range(0, tilePrefabs.Count)]);
        newTile.transform.position = new Vector3(x, 0, y);
        tileMap[x, y] = newTile;
        return newTile;
    }
}