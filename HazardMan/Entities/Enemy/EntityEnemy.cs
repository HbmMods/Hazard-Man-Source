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
           lock (World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.posX < posX + 1 && entity.posX > posX - 1 && entity.posY < posY + 1 && entity.posY > posY - 1)
                        {
                            ((EntityPlayer)entity).respawn();
                        }

                        if (entity.posY <= posY && entity.posX < posX + 2 && entity.posX > posX - 2 && onGround)
                        {
                            if (Library.isSoundActivated) Console.Beep(250, 200);
                            return true;
                        }
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
                renderer = new RenderEnemy();      

            base.Update();
        }
    }
}
