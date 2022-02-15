using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;


class LevelPart : GameObject
{
    Map levelPartMap;
    private float pixelWidth;
    private float pixelHeight;
    int tileWidth;
    int tileHeight;
    string spriteSheet;
    List<Tile> tiles = new List<Tile>();


    public LevelPart(String levelPartLocation, float startX, bool isStartOfLevel = false)
    {
        spriteSheet = levelPartLocation + "\\spritesheet.png";
        if (isStartOfLevel == false) levelPartMap = MapParser.ReadMap(levelPartLocation + "\\map.tmx");
        else levelPartMap = MapParser.ReadMap(levelPartLocation + "\\start.tmx");
        x = startX;
        BuildTiles();
    }


    void BuildTiles()
    {
        short[,] parsedTiles = levelPartMap.Layers[1].GetTileArray();
        pixelWidth = levelPartMap.TileWidth * parsedTiles.GetLength(0);
        pixelHeight = levelPartMap.TileHeight * parsedTiles.GetLength(1);
        tileWidth = levelPartMap.TileWidth;
        tileHeight = levelPartMap.TileHeight;
        for (int row = 0; row < parsedTiles.GetLength(1); ++row)
        {
            for (int col = 0; col < parsedTiles.GetLength(0); ++col)
            {
                int thisTileID = parsedTiles[col, row];
                if (thisTileID == 0) continue;
                Tile thisTile = new Tile(spriteSheet, 5, 5, parsedTiles[col, row] - 1);
                thisTile.x = col * tileWidth;
                thisTile.y = row * tileHeight;
                tiles.Add(thisTile);
                AddChild(thisTile);
            }

        }
    }

    public float GetWidth()
    {
        return pixelWidth;
    }

    public float GetHeight()
    {
        return pixelHeight; 
    }

    public float GetRightBorderX()
    {
        return x + pixelWidth;
    }

    public void MoveTiles(float moveX, float moveY = 0)
    {
        x += moveX;
    }

    public void KillMe()
    {

        foreach(Tile thisTile in tiles)
        {
            thisTile.Destroy();
        }
        this.Destroy();
    }
}