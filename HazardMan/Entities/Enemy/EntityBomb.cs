using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityBomb : EntityAI
    {
        public EntityBomb(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public override bool executeAICheck()
        {
            World.doLock(World.process_ee);

            foreach (Entity entity in World.entities)
            {
                if (entity is EntityPlayer)
                {
                    if (entity.posX < posX + 2 && entity.posX > posX - 2 && entity.posY < posY + 2 && entity.posY > posY - 2)
                    {
                        if (Library.isSoundActivated) Console.Beep();
                        World.unlock(World.process_ee);

                        return true;
                    }
                }
            }
            World.unlock(World.process_ee);
            return false;
        }

        public override void executeAITask()
        {
            setDead();
            Explosion exp = new Explosion(6, Explosion.explosiontype.ExplosionGeneric, (int)posX, (int)posY);
            exp.Destroy(exp.strength, exp.type, exp.x, exp.y);
        }

        public override void Update()
        {
            if (renderer == null)
                renderer = new RenderBomb();

            base.Update();
        }
    }
}
