using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Level : GameObject
{
    List<Tile> levelTiles = new List<Tile>();
    int tileWidth = 32;
    int tileHeight = 32;

    public Level()
    {
        for(int thisX = 0; thisX < game.width; thisX += tileWidth)
        {
            for(int thisY = 0; thisY < game.height; thisY += tileHeight)
            {
                int randomNumber = Utils.Random(0, 25);
                if (randomNumber == 25) --randomNumber;
                Tile thisTile = new Tile(randomNumber);
                thisTile.x = thisX;
                thisTile.y = thisY;
                levelTiles.Add(thisTile);
                AddChild(thisTile);
                Console.Write(randomNumber + "   ");
            }
            Console.Write('\n');
        }
    }

}
