using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    Tile[,] tileMap;

    // Start is called before the first frame update
    void Start()
    {
        int xCells = 10;
        int yCells = 10;
        tileMap = new Tile[xCells, yCells];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Populate new tile
    public void addTile(int x, int y, Tile tile)
    {
        tileMap[x, y] = tile;
    }
}
