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

        private int health = 2;
        public int maxHealth = 2;

        private bool isDead = false;

        public bool damaged = false;

        public RenderEntity renderer;

        public virtual void Update()
        {
            onGround = false;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            if (health <= 0)
            {
                this.setDead();
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
            {
                motionY -= 0.1F;
            }

            if (World.terrain[(int)posX, (int)posY + 1] is TerrainSpike && !(this is EntityProjectile))
            {
                setHealth(0);
            }
        }

        private bool isOutOfMap()
        {
            int inMotionPositionX = (int)(posX - motionX);
            int inMotionPositionY = (int)(posY - motionY);

            if (!(inMotionPositionX >= 0 && inMotionPositionX < World.terrain.GetLength(0) 
                && inMotionPositionY > 0 && inMotionPositionY < 29))
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

        public int getMaxHealth()
        {
            return this.maxHealth;
        }

        public void damage(int damage)
        {
            if (this.damaged == false)
            {
                this.damaged = true;
                new Thread(runSafe).Start();
                setHealth(getHealth() - damage);
            }
        }

        public virtual void setDead()
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

        private void runSafe()
        { 
            Thread.Sleep(2000);

            this.damaged = false;
        }
    }
}
