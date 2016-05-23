using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityBullet : EntityProjectile
    {
        public EntityBullet(float x, float y, float motionX, float motionY)
        {
            posX = x;
            posY = y;
            this.motionX = motionX;
            this.motionY = motionY;
        }

        public override bool executeAICheck()
        {
            lock(World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.posX < posX + 1 && entity.posX > posX - 1 && entity.posY < posY + 1 && entity.posY > posY - 1)
                        {
                            entity.damage(1);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void executeAITask()
        {
            setDead();
        }

        public override void Update()
        {
            motionY = 0F;
            motionX *= 2F;

            if (motionX < 0.1F && motionX > -0.1F)
                setDead();
            
            if (renderer == null)
                renderer = new RenderBullet();
            
            base.Update();
        }
    }
}
