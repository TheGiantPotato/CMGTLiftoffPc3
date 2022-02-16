using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
public class Spike : Trap
{
    public Spike(TiledObject obj = null) : base("spikes.png")
    {
        collider.isTrigger = true; //if don't put this code there is no collision happening, Why???
    }
}

