using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.IO.Ports;
using System.Threading;
using System.IO;
public class MyGame : Game
{
	Level currentLevel;
	string[] levelPaths = Directory.GetDirectories("levels");
	public MyGame() : base(1600, 900, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		currentLevel = new Level("levels\\lab");
		AddChild(currentLevel);
		targetFrameRate = 60;
		game.soundManager = new SoundManager();

	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		frameTime = Time.deltaTime;
		frameRate = 1000 / frameTime;
		if (Input.GetKey(Key.SPACE)) currentLevel.tempXVelocity = 1000;
		else if (Input.GetKey(Key.LEFT_CTRL)) currentLevel.tempXVelocity = 0;
		else currentLevel.tempXVelocity = 250;
		//Console.SetCursorPosition(0, 0);
		//Console.WriteLine(currentFps);


		if (currentLevel.pleaseKillMe == true)
        {
			swapLevel();	
        }

	}

	void swapLevel()
    {
		Player thisPlayer = currentLevel.player;
		int randomLevel = Utils.Random(0, levelPaths.Length);
		if (randomLevel == levelPaths.Length) --randomLevel;
		currentLevel.KillMe();
		currentLevel = new Level(levelPaths[randomLevel]);
		thisPlayer.x = currentLevel.GetPlayerStartPos().x;
		thisPlayer.y = currentLevel.GetPlayerStartPos().y;
		currentLevel.player = thisPlayer;
		
		AddChild(currentLevel);
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
		/*SerialPort port = new SerialPort();

		port.PortName = "COM4";

		port.BaudRate = 9600;

		port.RtsEnable = true;

		port.DtrEnable = true;

		port.Open();

		while (true)

		{

			string a = port.ReadExisting();

			if (a != "")

				Console.WriteLine("Read from port: " + a);


			if (Console.KeyAvailable)
			{

				ConsoleKeyInfo key = Console.ReadKey();

				port.Write(key.KeyChar.ToString());

			}

			Thread.Sleep(30);
		}*/
	}
}