using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class EntityBomb : EntityAI
    {
        private Explosion exp = null;

        public EntityBomb(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public override bool executeAICheck()
        {
           lock(World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.posX < posX + 2 && entity.posX > posX - 2 && entity.posY < posY + 2 && entity.posY > posY - 2)
                        {
                            if (Library.isSoundActivated) Console.Beep();  
                            return true;
                        }
                    }
                }
            }      
            return false;
        }

        public override void executeAITask()
        {
            if (exp == null)
            {
                setDead();
                exp = new Explosion(6, Explosion.explosiontype.ExplosionGeneric, (int)posX, (int)posY);
                exp.Damage(6, Explosion.explosiontype.ExplosionGeneric);
                exp.Destroy(exp.strength, exp.type);
            }
        }

        public override void Update()
        {
            if (renderer == null)
                renderer = new RenderBomb();

            base.Update();
        }
    }
}
