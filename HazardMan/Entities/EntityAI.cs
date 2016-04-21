using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    abstract class EntityAI : Entity
    {
        public abstract bool executeAICheck();

        public abstract void executeAITask();
    }
}
