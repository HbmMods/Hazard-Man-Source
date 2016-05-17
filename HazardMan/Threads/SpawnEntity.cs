using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class SpawnEntity
    {
        private Entity entity;

        public SpawnEntity(Entity entity)
        {
            this.entity = entity;
            new Thread(run).Start();
        }

        private void run()
        {
            lock(World.entities)
            {
                World.entities.Add(entity);
            }
        }
    }
}
