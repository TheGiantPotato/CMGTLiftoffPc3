using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using System.Drawing;
public class HUD : Canvas
{
    Player player;
    //EasyDraw healthBar;
    AnimationSprite lives;
    int currentLife;
    int framesToAnimate;

    public HUD(Player pTarget) : base(100, 100, false)
    {
        player = pTarget;
        currentLife = player.Health;
        /*healthBar = new EasyDraw(player.health * 2, 20, false);
        healthBar.x = 5;
        AddChild(healthBar);*/
       
        //TextSize(10);

        lives = new AnimationSprite("heartsAnimation.png", 1, 21, -1, false, false);
        lives.SetCycle(0, 21);
        lives.SetScaleXY(0.15f, 0.15f);
        AddChild(lives);
        framesToAnimate = 0;
    }

    void Update()
    {
        /*healthBar.Clear(Color.Red);
        healthBar.Fill(Color.Green);
        healthBar.Rect(0, 0, player.health * 4, 40); */

        /*ClearTransparent();
        Fill(Color.White);
        Text("Score: " + player.coins, 5, 50);*/

        /* for (int i = 0; i < 5; i++)
         {
             lives = new Sprite("heart.png", false);
             SetXY(i * 100, 10);
             AddChild(lives);
         }*/

        if (framesToAnimate > 20) framesToAnimate = 0;
        if (currentLife > player.Health)
        {
            currentLife = player.Health;
            framesToAnimate += 1;
        }
        if (framesToAnimate > lives.currentFrame)
        {
            lives.Animate(0.12f);
        }

        graphics.Clear(Color.Empty);
        graphics.DrawString("Score  " + player.coins, SystemFonts.DefaultFont, Brushes.AntiqueWhite, 5, 50);
        

    }
}
