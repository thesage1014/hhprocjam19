using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {
    public List<Tile> tilePrefabs;
    static int xCells = 10;
    static int yCells = 10;
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
    public Tile getTile(int x, int z)
    {
        if (x <= xCells || x < 0 || z >= yCells || z < 0)
        {
            Debug.LogError("Tile seach out of bounds:" + x + " " + z);
            return null;
        }
        else
        {
            Tile returnTile = tileMap[x, z];
            return returnTile;
        }
    }
    // Populate new tile
    void addTile(int x, int z, Tile tile)
    {
        Tile newtile = Instantiate<Tile>(tilePrefabs[Random.Range(0, tilePrefabs.Count)]);
        newtile.transform.position = new Vector3(x, 0, z);
        tileMap[x, z] = tile;
    }
}
