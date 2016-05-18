﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityShooter : EntityAI
    {
        Random rand;
        public int step = 0;

        public EntityShooter(float x, float y)
        {
            rand = new Random();
            posX = x;
            posY = y;
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
            EntityBullet bullet = new EntityBullet(this.posX, this.posY, rand.Next(2) == 0 ? 1 : -1, 0);
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
