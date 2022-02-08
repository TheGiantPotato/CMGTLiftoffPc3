using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Tile : AnimationSprite
{
    public Tile(int tileID) : base("nethertileset.png", 5, 5)
    {
        
        SetFrame(tileID);

    }
}
