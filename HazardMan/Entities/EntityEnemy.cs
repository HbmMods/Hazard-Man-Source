using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityEnemy : EntityAI
    {
        public override void executeAICheck()
        {
        }

        public override void executeAITask()
        {
        }

        public override void Update()
        {
            if (renderer == null)
            {
                renderer = new RenderEnemy();
            }

            base.Update();
        }
}
