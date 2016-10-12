using System;

namespace HazardMan
{
    class EntitySprayer : EntityAI
    {
        Random rand = new Random();
        public int step;

        public EntitySprayer(float x, float y)
        {
            setX(x);
            setY(y);
        }

        public override bool executeAICheck()
        {
            step++;

            if (step >= 60)
                step = 0;

            return step <= 0;
        }

        public override void executeAITask()
        {
            EntityFragment frag1 = new EntityFragment(this.getX(), this.getY() - 1, 0.5F, 0.75F);
            EntityFragment frag2 = new EntityFragment(this.getX(), this.getY() - 1, 0F, 0.75F);
            EntityFragment frag3 = new EntityFragment(this.getX(), this.getY() - 1, -0.5F, 0.75F);
            new SpawnEntity(frag1);
            new SpawnEntity(frag2);
            new SpawnEntity(frag3);
            if(rand.Next(5) == 0)
            {
                EntityFragment frag4 = new EntityFragment(this.getX(), this.getY() - 1, 0.25F, 0.75F);
                EntityFragment frag5 = new EntityFragment(this.getX(), this.getY() - 1, -0.25F, 0.75F);
                new SpawnEntity(frag4);
                new SpawnEntity(frag5);
            }
        }

        public override void Update()
        {
            if (renderer == null)
                renderer = new RenderSprayer();        

            base.Update();
        }
    }
}
