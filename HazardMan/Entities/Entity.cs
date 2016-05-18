using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

        private int health = 10;
        public int maxHealth = 10;

        private bool isDead = false;

        public RenderEntity renderer;

        public virtual void Update()
        {
            onGround = false;

            if (health > maxHealth)
                health = maxHealth;

            if (health <= 0)
            {
                if (this is EntityPlayer)
                {
                    ((EntityPlayer)this).respawn();
                }
                else
                {
                    this.setDead();
                }
            }

            lastPosX = posX;
            lastPosY = posY;

            if (isOutOfMap())
            {
                setHealth(0);
                return;
            }

            if (!(World.terrain[(int)(posX - motionX), (int)(posY - motionY)] != null))
            {
                posX -= motionX;
                posY -= motionY;
                onGround = false;
            }
            else
            {
                motionX = 0;
                motionY = 0;
                onGround = true;
            }

            renderer.renderEntityAt(this);

            motionX *= 0.5F;

            if (!onGround)
                motionY -= 0.1F;

            if (World.terrain[(int)posX, (int)posY + 1] is TerrainSpike && !(this is EntityProjectile))
            {
                if (this is EntityPlayer)
                    Console.Beep();

                setHealth(0);
            }
        }

        private bool isOutOfMap()
        {
            if (!((int)(posX - motionX) > 0 && (int)(posX - motionX) < World.terrain.GetLength(0) && posY > -1 && posY < 30))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void damage(int damage)
        {
            setHealth(getHealth() - damage);
        }

        public void setDead()
        {
            if (!isDead)
            {
                isDead = true;
                new Thread(runDie).Start();
            }
        }

        private void runDie()
        {
            lock (World.entities)
            {
                WorldThread.wantToDie.Add(this);
            }
        }
    }
}
