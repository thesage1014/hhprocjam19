using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    
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

        Tile returnTile = tileMap[x,z];

        return returnTile;
    }
    // Populate new tile
    public void addTile(int x, int z, Tile tile)
    {
        tileMap[x, z] = tile;
    }
}
