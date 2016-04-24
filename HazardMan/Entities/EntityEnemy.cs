using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityEnemy : EntityAI
    {
        public EntityEnemy(float x, float y)
        {
            posX = x;
            posY = y;
        }

        public override bool executeAICheck()
        {
            foreach(Entity p in World.entities)
            {
                if(p is EntityPlayer)
                {

                    if(p.posX < posX + 1 && p.posX > posX - 1 && p.posY < posY + 1 && p.posY > posY - 1)
                    {
                        p.health = 0;
                    }

                    if(p.posY <= posY && p.posX < posX + 2 && p.posX > posX - 2 && onGround)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void executeAITask()
        {
            motionY += 1;
        }

        public override void Update()
        {
            if (renderer == null)
            {
                renderer = new RenderEnemy();
            }

            base.Update();
        }
    }
}
