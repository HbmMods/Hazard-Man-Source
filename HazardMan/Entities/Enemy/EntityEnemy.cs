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
            setX(x);
            setY(y);
        }

        public override bool executeAICheck()
        {
           lock (World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.getX() < getX() + 1 && entity.getX() > getX() - 1 && entity.getY() < getY() + 1 && entity.getY() > getY() - 1)
                        {
                            entity.setDead();
                        }

                        if (entity.getY() <= getY() && entity.getX() < getX() + 2 && entity.getX() > getX() - 2 && onGround)
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
