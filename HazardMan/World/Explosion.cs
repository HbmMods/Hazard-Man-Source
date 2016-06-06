using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class Explosion
    {
        public enum explosiontype { ExplosionGeneric };
        public explosiontype type;
        public int strength;
        public int x;
        public int y;
        public int radius;
        public int posX;
        public int posY;

        private List<TerrainElement> blocks = new List<TerrainElement>();

        public Explosion(int strength, explosiontype type, int x, int y)
        {
            this.strength = strength;
            this.type = type;
            this.x = x;
            this.y = y;
        }

        public void Damage(int strength, explosiontype type, int x, int y)
        {

        }

        public void Destroy(int radius, explosiontype type, int posX, int posY)
        {
            this.radius = radius;
            this.posX = posX;
            this.posY = posY;

            for (int y = -radius; y <= radius; y++)
            {
                for (int x = -radius; x <= radius; x++)
                {
                    if (x * x + y * y <= radius * radius)
                    {
                        if (World.terrain[posX + x, posY + y] != null)
                        {

                            foreach (Entity entity in World.entities)
                            {
                                if (entity is EntityPlayer)
                                {
                                    if (entity.posX < posX + 2 && entity.posX > posX - 2 && entity.posY < posY + 2 && entity.posY > posY - 2)
                                    {
                                        ((EntityPlayer)entity).setDead();
                                    }
                                }
                            }

                            //blocks.Add(World.terrain[posX + x, posY + y]);

                            World.changeTerrainElement(posX + x, posY + y, null);
                        }
                    }
                }
            }

            //new Thread(remake).Start();
        }

        public void Particle(int strength, explosiontype type, int x, int y)
        {

        }

        private void remake()
        {
            Thread.Sleep(2 * 1000);

            for(int i = blocks.Count-1; i > 0; i--)
            {
                if(blocks[i] != null)
                {
                    TerrainElement element = blocks[i];

                    World.terrain[element.getX(), element.getY()] = new TerrainSolid(element.getX(), element.getY());

                    Console.CursorLeft = element.getX();
                    Console.CursorTop = element.getY();

                    World.terrain[element.getX(), element.getY()].renderElement();

                    Thread.Sleep(100);
                }
            }
        }
    }
}
