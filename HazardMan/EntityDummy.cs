using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityDummy : Entity
    {
       

        public EntityDummy(float x, float y, ConsoleKey up, ConsoleKey left, ConsoleKey right) : base (up, left, right)
        {
            posX = x;
            posY = y;
        }

        //This is a test

        public override void Update()
        {
            if (renderer == null)
            {
                renderer = new RenderDummy();
            }

            base.Update();       
        }
    }
}
