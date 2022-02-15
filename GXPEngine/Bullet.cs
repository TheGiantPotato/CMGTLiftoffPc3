using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Bullet : AnimationSprite
{
    //the collision check for bullet is destroying each other(FIXED)
    //MUST FIX: bullet from shooter can destroy barrier

    public Sprite owner;
    int speedX;

    
    public Bullet(string filename, int columns, int rows, Sprite pOwner, int pVelocityX/*, int pVelocityY*/) : base(filename, columns, rows)
    {
        owner = pOwner;
        SetXY(pOwner.x, pOwner.y);
        SetOrigin(pOwner.width/2, pOwner.height/2);
        speedX = pVelocityX;
    }

    void Update()
    {
        BulletMovement();
    }

    void BulletMovement()
    {
        this.x += speedX;
    }

    void OnCollision(GameObject objectsColliding)
    {
        if (objectsColliding != owner)
        {
            if (objectsColliding is Player player)
            {
                player.Health -= 1;
                this.LateDestroy();
                Console.WriteLine("Bullet hits Player, Player's health: " + player.Health);
            }

            if (objectsColliding is Shooter shooter)
            {
                //shooter.health -= 5;
                this.LateDestroy();
                shooter.LateDestroy();
                //Console.WriteLine("Bullet hits Shooter, Shooter's health: " + shooter.health);
            }

            /*if (objectsColliding is Barrier barrier)
            {
                barrier.health -= 5;
            }*/
        }
    }
}
