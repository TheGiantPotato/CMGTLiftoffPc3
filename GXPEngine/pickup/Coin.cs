using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
public class Coin : Pickup
{
    public string coinType;
    public Coin(/*string coinTypeFile,*/ TiledObject obj = null) : base("coins", 7, 3)
    {
        if (obj != null)
        {
            coinType = obj.GetStringProperty("coinType", null);
        }

        switch (coinType)
        {
            case "bronze":
                SetCycle(0, 6);
                break;
            case "silver":
                SetCycle(14, 6);
                break;
            case "gold":
                SetCycle(7, 6);
                break;
        }
    }

    void Update()
    {
        Animate(0.08f);
    }
}
