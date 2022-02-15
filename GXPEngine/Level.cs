using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GXPEngine;



class Level : GameObject
{
    List<Tile> levelTiles = new List<Tile>();
    List<Tile> lateLevelTiles = new List<Tile>();
    List<LevelPart> activeLevelParts = new List<LevelPart>();
    int levelHeight;
    public float currentLevelXPos = 0;
    int tileWidth = 32;
    int tileHeight = 32;
    public int tempXVelocity = 250;
    string levelPath;
    string[] levelParts;
    Sound shootSound = new Sound("Single_Shot.wav");
    public Player player;
    public bool pleaseKillMe = false;

    Vector playerStartPos = new Vector(100, 100);

    public Level(string levelPath)
    {
        this.levelPath = levelPath;
        activeLevelParts.Add(new LevelPart(levelPath, 0, true));
        AddChild(activeLevelParts[0]);
        currentLevelXPos += activeLevelParts[0].GetWidth();
        levelParts = Directory.GetDirectories(levelPath + "\\parts");
        while (currentLevelXPos < game.width)
        {
            AddNewLevelPart();
        }

        SetupPlayer();

    }

    public Vector GetPlayerStartPos()
    {
        return playerStartPos;
    }


    void Update()
    {
        float distToMove = -tempXVelocity / game.targetFrameRate;
        foreach (LevelPart thisPart in activeLevelParts)
        {
            thisPart.MoveTiles(distToMove);
        }
        currentLevelXPos += distToMove;
        while (currentLevelXPos < game.width)
        {
            AddNewLevelPart();
        }
        if (activeLevelParts[0].GetRightBorderX() < 0)
        {
            activeLevelParts[0].KillMe();
            activeLevelParts[0].Remove();
            activeLevelParts.Remove(activeLevelParts[0]); 
        }
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(activeLevelParts.Count);
        foreach(LevelPart thisPart in activeLevelParts)
        {
            Console.WriteLine(thisPart.x + "\n" + thisPart.GetChildren().Count() + "\n");

        }
        if (Input.GetKey(Key.T))
        {
            game.soundManager.PlaySound(shootSound, "level");
        }
    }

    void AddNewLevelPart()
    {
        int randomLevelPartNumber = Utils.Random(0, levelParts.Length);
        if (randomLevelPartNumber == levelParts.Length) --randomLevelPartNumber;

        LevelPart thisLevelPart = new LevelPart(levelParts[randomLevelPartNumber], currentLevelXPos);
        activeLevelParts.Add(thisLevelPart);
        AddChild(thisLevelPart);
        currentLevelXPos += thisLevelPart.GetWidth();
    }


    void SetupPlayer()
    {

    }

    public void KillMe()
    {
        foreach(LevelPart thisPart in activeLevelParts)
        {
            thisPart.KillMe();
        }
        this.Destroy();
    }



}
