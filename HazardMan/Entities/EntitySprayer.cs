using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntitySprayer : EntityAI
    {
        Random rand = new Random();

        public int step = 0;

        public EntitySprayer(float x, float y)
        {
            posX = x;
            posY = y;
        }

        public override bool executeAICheck()
        {
            step++;

            if (step >= 50)
                step = 0;

            return step <= 0;
        }

        public override void executeAITask()
        {
            EntityFragment frag1 = new EntityFragment(this.posX, this.posY - 1, 0.5F, 0.75F);
            EntityFragment frag2 = new EntityFragment(this.posX, this.posY - 1, 0F, 0.75F);
            EntityFragment frag3 = new EntityFragment(this.posX, this.posY - 1, -0.5F, 0.75F);
            World.spawnEntityInWorld(frag1);
            World.spawnEntityInWorld(frag2);
            World.spawnEntityInWorld(frag3);
            if(rand.Next(5) == 0)
            {
                EntityFragment frag4 = new EntityFragment(this.posX, this.posY - 1, 0.25F, 0.75F);
                EntityFragment frag5 = new EntityFragment(this.posX, this.posY - 1, -0.25F, 0.75F);
                World.spawnEntityInWorld(frag4);
                World.spawnEntityInWorld(frag5);
            }
        }

        public override void Update()
        {
            if (renderer == null)
            {
                renderer = new RenderSprayer();
            }

            base.Update();
        }
    }
}
