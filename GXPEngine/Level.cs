using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GXPEngine;



class Level : GameObject
{
    
    List<LevelPart> activeLevelParts = new List<LevelPart>();
    List<Background> backGroundLayers = new List<Background>();

    
    public float currentLevelXPos = 0;
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
        SetupBackgrounds();
        activeLevelParts.Add(new LevelPart(levelPath, 0, true));
        AddChild(activeLevelParts[0]);
        currentLevelXPos += activeLevelParts[0].GetWidth();
        levelParts = Directory.GetFiles(levelPath + "\\parts");
        while (currentLevelXPos < game.width)
        {
            AddNewLevelPart();
        }
        player = activeLevelParts[0].FindObjectOfType<Player>();
        CreateHUD(player);
        Shooter[] shooters = activeLevelParts[0].FindObjectsOfType<Shooter>();
        {
            foreach (Shooter s in shooters)
            {
                s.target = player;
            }
        }

        SetupPlayer();
    }
    void CreateHUD(Player pTarget)
    {
        if (player == null) return; //if player is null then don't excecute what's underneath
        game.LateAddChild(new HUD(pTarget));
    }

    public Vector GetPlayerStartPos()
    {
        return playerStartPos;
    }

    void SetupBackgrounds()
    {
        string[] backgroundImages = Directory.GetFiles(levelPath + "\\background");
        string[] partBackgroundLocations = Directory.GetDirectories(levelPath + "\\background");
        float parallaxValue = 0.8f;
        float thisBackgroundMovementSpeed = parallaxValue;
        for (int i = 0; i < backgroundImages.Length + partBackgroundLocations.Length; ++i)
        {
            thisBackgroundMovementSpeed *= parallaxValue;
        }
        foreach (string thisBackgroundImage in backgroundImages)
        {
            Background thisBackground = new Background(thisBackgroundImage, thisBackgroundMovementSpeed);
            backGroundLayers.Add(thisBackground);
            AddChild(thisBackground);
            thisBackgroundMovementSpeed *= 1 / parallaxValue;
        }
        foreach (string thisPartBackgroundLocation in partBackgroundLocations)
        {
            Background thisPartBackground = new PartBackground(thisPartBackgroundLocation, thisBackgroundMovementSpeed);
            backGroundLayers.Add(thisPartBackground);
            AddChild(thisPartBackground);
            thisBackgroundMovementSpeed *= 1 / parallaxValue;
        }
    }

    void moveBackgrounds(float distanceToMove)
    {
        foreach(Background thisBackground in backGroundLayers)
        {
            thisBackground.DoRelativeMovement(distanceToMove);
        }
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
        
        if (Input.GetKey(Key.T))
        {
            game.soundManager.PlaySound(shootSound, "level");
        }
        moveBackgrounds(distToMove);
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
        foreach(Background thisBackground in backGroundLayers)
        {
            thisBackground.KillMe();
        }
        this.Destroy();
    }



}
