using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    abstract class Entity
    {
        public float motionX;
        public float motionY;

        public float posX;
        public float posY;
        public float lastPosX;
        public float lastPosY;

        public int health = 10;
        public int maxHealth = 10;

        public RenderEntity renderer;

        public virtual void Update()
        {

        }

        public virtual void KillEntity()
        {
            World.kill.Add(this);
        }
    }
}
