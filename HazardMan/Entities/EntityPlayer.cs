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
        private ConsoleColor color;
        private string name;

        public EntityPlayer(float x, float y, ConsoleKey up, ConsoleKey left, ConsoleKey right, ConsoleColor color, string name)
        {
            posX = x;
            posY = y;

            this.up = up;
            this.left = left;
            this.right = right;
            this.color = color;
            this.name = name;
        }

        public ConsoleColor getColor()
        {
            return this.color;
        }

        public string getName()
        {
            return this.name;
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

            if (!(posX < 119))
            {
                foreach(OptionPlayer player in Library.players)
                {
                    if(this.getName() == player.getName())
                    {
                        Library.score[player] += 1;
                        Library.addLevel();
                        Library.recreateWorld = true;

                        World.StopWorld();
                        break;
                    }
                }
            }

            if (World.terrain[((int)posX), ((int)posY)] is TerrainSolid)
                respawn();

            if (renderer == null)
                renderer = new RenderPlayer(this);
            

            base.Update();
        }

        public void respawn()
        {
            base.setDead();

            new SpawnEntity(new EntityPlayer(1, Console.WindowHeight / 2 - 1, this.up, this.left, this.right, this.getColor(), this.getName()));
        }

        public override void checkOut()
        {
            if (!(posX > 0 && posY > -1 && posY < 30))
                this.respawn();
        }
    }
}
