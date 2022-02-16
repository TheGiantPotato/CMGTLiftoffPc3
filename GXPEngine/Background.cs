using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.IO;
class Background : Sprite
{
    protected float relativeSpeed;
    protected float rightBorderX;

    public Background(string imageLink, float relativeSpeed = 1, float startX = 0) : base(imageLink)
    {
        this.relativeSpeed = relativeSpeed;
        rightBorderX += width;
    }


    public virtual void DoRelativeMovement(float baseXMovement, float baseYMovement = 0)
    {
        this.x -= baseXMovement * (1 - relativeSpeed);
        this.y -= baseYMovement * (1 - relativeSpeed);
    }

    public virtual void KillMe()
    {
        this.Destroy();
    }

}

class PartBackground : Background
{
    String[] listOfPossibleBackgroundParts;
    List<BackgroundPart> activeParts = new List<BackgroundPart>();
    public PartBackground(string folderLink, float relativeSpeed = 1) : base("empty.png", relativeSpeed)
    {
        
        listOfPossibleBackgroundParts = Directory.GetFiles(folderLink);
    }

    void Update()
    {

        CheckForSectionDelete();
        while(rightBorderX < game.width * 2)
        {
            AddBackgroundPart();
        }
        Console.WriteLine(rightBorderX);
    }

    public override void DoRelativeMovement(float baseXMovement, float baseYMovement = 0)
    {
        foreach (BackgroundPart thisBackgroundPart in activeParts)
        {
            thisBackgroundPart.x += baseXMovement * relativeSpeed;
            thisBackgroundPart.y += baseYMovement * relativeSpeed;

            

        }
        rightBorderX += baseXMovement * relativeSpeed;
    }

    void CheckForSectionDelete()
    {
        if (activeParts.Count() > 0 && parent.x + activeParts[0].x + activeParts[0].width < 0)
        {
            activeParts[0].Destroy();
            activeParts.Remove(activeParts[0]);
        }
    }

    void AddBackgroundPart()
    {
        int randomNum = Utils.Random(0, listOfPossibleBackgroundParts.Length);
        if (randomNum == listOfPossibleBackgroundParts.Length) --randomNum;
        BackgroundPart thisBackgroundPart = new BackgroundPart(listOfPossibleBackgroundParts[randomNum]);
        activeParts.Add(thisBackgroundPart);
        thisBackgroundPart.x = rightBorderX;
        AddChild(thisBackgroundPart);
        rightBorderX += thisBackgroundPart.width;
    }

    public override void KillMe()
    {
        foreach(BackgroundPart thisPart in activeParts)
        {
            thisPart.Destroy();
        }

        base.KillMe();
    }

}

class BackgroundPart : Sprite
{

    
    public BackgroundPart(string imageLink) : base(imageLink)
    {

    }
}