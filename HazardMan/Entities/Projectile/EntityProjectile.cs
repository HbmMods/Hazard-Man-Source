using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    abstract class EntityProjectile : EntityAI
    {
        public override bool executeAICheck() { return false; }

        public override void executeAITask() { }
    }
}
