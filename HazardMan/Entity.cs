﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    abstract class Entity
    {
        public bool onGround;

        public float motionX;
        public float motionY;

        public float posX;
        public float posY;
        public float lastPosX;
        public float lastPosY;

        public int health = 10;
        public int maxHealth = 10;

        public RenderEntity renderer;

        private ConsoleKey up;
        private ConsoleKey left;
        private ConsoleKey right;

        public Entity(ConsoleKey up, ConsoleKey left, ConsoleKey right)
        {
            this.up = up;
            this.left = left;
            this.right = right;
        }

        public virtual void Update()
        {
            if (World.input != ConsoleKey.Delete)
            {
                if ((World.input == up) && onGround)
                {
                    motionY += 1;
                }
                if (World.input == left)
                {
                    motionX += 1;
                }
                if (World.input == right)
                {
                    motionX -= 1;
                }
                World.input = ConsoleKey.Delete;
            }

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                KillEntity();
            }

            lastPosX = posX;
            lastPosY = posY;

            if (!(World.terrain[(int)(posX - motionX), (int)(posY - motionY)] != null))
            {
                posX -= motionX;
                posY -= motionY;
                onGround = false;
            }
            else
            {
                motionY *= 0.5F;
                onGround = true;
            }

            if (!(posX > -1 && posX < 120 && posY > -1 && posY < 30))
            {
                KillEntity();
            }

            renderer.renderEntityAt(this);

            motionX *= 0.5F;

            if (!onGround)
            {
                motionY -= 0.1F;
            }
        }

        public virtual void KillEntity()
        {
            World.kill.Add(this);
        }
    }
}
