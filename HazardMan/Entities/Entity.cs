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

        public RenderEntity renderer;

        public virtual void Update()
        {
            onGround = false;

            if (health > maxHealth)
                health = maxHealth;

            if (health <= 0)
                setDead();  

            lastPosX = posX;
            lastPosY = posY;

            if (posX <= 0)
            {
                health = 0;
                return;
            }

            if (posX >= World.terrain.GetLength(0) - 1)
            {
                health = 0;
                return;
            }

            try {
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
            } catch
            {
                this.setDead();
            }

            checkOut();

            renderer.renderEntityAt(this);

            motionX *= 0.5F;

            if (!onGround)
                motionY -= 0.1F;

            if(World.terrain[(int)posX, (int)posY + 1] is TerrainSpike && !(this is EntityProjectile))
            {
                if(this is EntityPlayer)
                {
                    Console.Beep();
                    ((EntityPlayer)this).respawn();
                } else
                {
                    setDead();
                }
            }
            
        }

        public virtual void checkOut()
        {
            if (!(posX > 0 && posY > -1 && posY < 30))
                setDead();
            
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
            this.health =- damage;
        }

        public void setDead()
        {
            new Thread(runDie).Start();
        }

        private void runDie()
        {
            World.doLock(World.process_de);

            WorldThread.wantToDie.Add(this);

            World.unlock(World.process_de);
        }
    }
}
