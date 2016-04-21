using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class EntityPlayer : Entity
    {
        private ConsoleKey up;
        private ConsoleKey left;
        private ConsoleKey right;

        public EntityPlayer(float x, float y, ConsoleKey up, ConsoleKey left, ConsoleKey right)
        {
            posX = x;
            posY = y;

            this.up = up;
            this.left = left;
            this.right = right;
        }

        public override void Update()
        {
            if (World.input != ConsoleKey.Delete)
            {
                if ((World.input == up) && onGround)
                {
                    motionY += 1;
                    World.input = ConsoleKey.Delete;
                }
                if (World.input == left)
                {
                    motionX += 1;
                    World.input = ConsoleKey.Delete;
                }
                if (World.input == right)
                {
                    motionX -= 1;
                    World.input = ConsoleKey.Delete;
                }
            }

            if (renderer == null)
            {
                renderer = new RenderPlayer();
            }

            base.Update();
        }
    }
}
