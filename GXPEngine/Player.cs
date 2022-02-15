using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;
public class Player : AnimationSprite
{
    //Figure out how to make jumping & double jumping with a timer

    //FIX:
    //Clean up the code for HorizontalMovement

    //HorizontalMovement
    float speedX = 0;
    float horizontalSpeed = 0.05f;
    bool isSliding = false;
    bool isMirrored;
    bool isRunning;

    //VerticalMovement
    float verticalSpeed;
    bool isGrounded;
    int numberOfJump;
    int maxNumberOfJump = 1;

    //Shooting
    int lastTimeShot = 0;
    int lastTimeAttacked = 0;

    //Collectables
    int _health = 10;
    public int Health { get => _health; set => _health = value; }
    //int bullets;
    int bulletSpeed = 5;
    public int coins = 0;

    public Player(TiledObject obj) : base("player.png", 11, 1)
    {
        collider.isTrigger = true;
    }

    void Update()
    {
        Animate(0.12f);
        HorizontalMovement();
        VerticalMovement();
        if (lastTimeShot < Time.time) Shooting();
        CheckHealth();
    }

    void HorizontalMovement()
    {
        //speedX *= 0.35f;
        speedX = 1.75f;

        //Normal Movement
        speedX += horizontalSpeed;
        Console.WriteLine(speedX);

        //Sliding
        if (Input.GetKey(Key.S)) isSliding = true;
        if (Input.GetKeyUp(Key.S)) isSliding = false;

        if (isSliding) speedX += 3;

        //Animation for HorizontalMovement
        if (Mathf.Abs(speedX) > 0.1f)
        {
            if (isSliding) SetCycle(2, 1);
            else SetCycle(9, 2);            //Running animation
        }
        else SetCycle(3, 1);

        MoveUntilCollision(speedX, 0);
    }

    void VerticalMovement()
    {
        verticalSpeed += 0.19f;

        if ((Input.GetKeyDown(Key.W)))
        {
            if (numberOfJump > 0)
            {
                verticalSpeed = -10.5f;
                --numberOfJump;
            }
            else maxNumberOfJump = 1;
        }

        isGrounded = false;

        if (MoveUntilCollision(0, verticalSpeed) != null)
        {
            verticalSpeed = 0;
            isGrounded = true;
            numberOfJump = maxNumberOfJump;
        }
    }

    void OnCollision(GameObject objectsColliding)
    {
        //Collisions - Pickup
        if (objectsColliding is DoubleJump doubleJump)
        {
            maxNumberOfJump = 2;
            doubleJump.LateDestroy();
            Console.WriteLine("Player takes Double Jump Pickup");
        }

        if (objectsColliding is Coin coin)
        {
            if (coin.coinType == "gold") coins += 3;
            else if (coin.coinType == "silver") coins += 2;
            else if (coin.coinType == "bronze") coins += 1;

            coin.LateDestroy();
            Console.WriteLine("Player gets a coin: " + coin.coinType);
            Console.WriteLine("Player's coins: " + coins);
        }

        //Collisions - Enemy

        //Collisions - Trap
        if (objectsColliding is Spike)
        {
            if (lastTimeAttacked < Time.time)
            {
                //ReceivedDamage(enemy.Damage);
                int spikeDamage = 1;
                this._health -= spikeDamage;
                Console.WriteLine("Player hits Spikes, Player's Health: " + _health);
                lastTimeAttacked = Time.time + 1000;
                //Console.WriteLine("Received Damage: " + enemy.Damage);
            }
        }
    }

    void Shooting()
    {
        if (Input.GetKey(Key.SPACE))
        {
            Bullet bullet = new Bullet(isMirrored ? "fireballMirrored.png" : "fireball.png", 1, 1, this, isMirrored ? -bulletSpeed : bulletSpeed);
            parent.AddChild(bullet);
            lastTimeShot = Time.time + 500;
            Console.WriteLine("Player shooting bullet");
        }

    }

    void CheckHealth()
    {
        if (_health <= 0)
        {
            //((MyGame)game).LoadLevel("testMap");
        }
    }

    public void AddForce(float fX, float fY)
    {
        speedX += fX;
        y += fY;
    }
}
