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
        public uint id;
        private ConsoleColor color;

        public EntityPlayer(float x, float y, ConsoleKey up, ConsoleKey left, ConsoleKey right, uint id, ConsoleColor color)
        {
            posX = x;
            posY = y;

            this.up = up;
            this.left = left;
            this.right = right;
            this.id = id;
            this.color = color;
        }

        public ConsoleColor getColor()
        {
            return this.color;
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
                renderer = new RenderPlayer(this);
            }

            base.Update();
        }
    }
}
