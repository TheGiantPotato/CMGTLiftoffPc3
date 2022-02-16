using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Tile : AnimationSprite
{
    public Tile(string tileSet, int tileSetWidth, int tileSetHeight, int tileID) : base(tileSet, tileSetWidth, tileSetHeight)
    {
        
        SetFrame(tileID);

    }
}
