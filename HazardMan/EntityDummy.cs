using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityDummy : Entity
    {
        public bool onGround;
        public EntityDummy(float x, float y)
        {
            posX = x;
            posY = y;
        }

        public override void Update()
        {
            if(World.input != ConsoleKey.Delete)
            {
                if(World.input == ConsoleKey.W && onGround)
                {
                    motionY += 1;
                }
                if (World.input == ConsoleKey.A)
                {
                    motionX += 1;
                }
                if (World.input == ConsoleKey.D)
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

            if (!(World.terrain[(int)(posX -= motionX), (int)(posY -= motionY)] != null))
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

            posX += motionX;
            posY += motionY;

            if (!(posX > -1 && posX < 120 && posY > -1 && posY < 30))
            {
                KillEntity();
            }

            if (renderer == null)
            {
                renderer = new RenderDummy();
            }

            renderer.renderEntityAt(this);
            
            motionX *= 0.5F;

            if(!onGround)
            {
                motionY -= 0.1F;
            }
        }
    }
}
