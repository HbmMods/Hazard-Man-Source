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
            setX(x);
            setY(y);
        }

        public override bool executeAICheck()
        {
           lock(World.entities)
            {
                foreach (Entity entity in World.entities)
                {
                    if (entity is EntityPlayer)
                    {
                        if (entity.getX() < this.getX() + 2 && entity.getX() > this.getX() - 2 && entity.getY() < this.getY() + 2 && entity.getY() > this.getY() - 2)
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
                exp = new Explosion(6, Explosion.explosiontype.ExplosionGeneric, (int)this.getX(), (int)this.getY());
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
