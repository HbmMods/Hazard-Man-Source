using System;

namespace HazardMan
{
    class EntityShooter : EntityAI
    {
        Random rand;
        public int step;

        public EntityShooter(float x, float y)
        {
            rand = new Random();
            
            setX(x);
            setY(y);
        }

        public override bool executeAICheck()
        {
            step++;

            if (step >= 30)
                step = 0;

            return step <= 0;
        }

        public override void executeAITask()
        {
            EntityBullet bullet = new EntityBullet(this.getX(), this.getY(), rand.Next(2) == 0 ? 1 : -1, 0);
            new SpawnEntity(bullet);
        }

        public override void Update()
        {
            if (renderer == null)
                renderer = new RenderShooter();

            base.Update();
        }
    }
}
