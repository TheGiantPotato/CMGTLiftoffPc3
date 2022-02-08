using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Player : AnimationSprite
{
    float speed = 3;
    float horizontalSpeed = 3;
    
    public Player() : base("barry.png", 7, 1)
    {
        collider.isTrigger = true;
    }

    void Update()
    {
        Animate(0.2f);
        horizontalMovement();

       
    }

    void horizontalMovement()
    {
        float speedX = 0;

        if (Input.GetKey(Key.RIGHT))
        {
            speedX += horizontalSpeed;
            Mirror(false, false);
            
        }

        if (Input.GetKey(Key.LEFT))
        {
            speedX -= horizontalSpeed;
            Mirror(true, false);
            
        }

        if (Mathf.Abs(speedX) > 0.1f) SetCycle(0, 5);
        else SetCycle (4, 3);

        MoveUntilCollision(speedX, 0);
    }
}
