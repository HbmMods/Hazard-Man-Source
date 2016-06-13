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
        public int radius;
        public int posX;
        public int posY;

        private List<TerrainElement> blocks = new List<TerrainElement>();

        public Explosion(int strength, explosiontype type, int posX, int posY)
        {
            this.strength = strength;
            this.type = type;
            this.posX = posX;
            this.posY = posY;
        }

        public void Damage(int strength, explosiontype type)
        {

        }

        public void Destroy(int radius, explosiontype type)
        {
            // Set radius
            this.radius = radius;

            // For y axe
            for (int y = -radius; y <= radius; y++)
            {
                // For x axe
                for (int x = -radius; x <= radius; x++)
                {
                    if (x * x + y * y <= radius * radius)
                    {
                        if (!IsOutOfMap(x, y))
                        {

                            // Check if terrain isn't null
                            if (World.terrain[posX + x, posY + y] != null)
                            {
                                // Check all entiy
                                foreach (Entity entity in World.entities)
                                {
                                    // Is in radius
                                    if (entity.posX < posX + 4 && entity.posX > posX - 4 && entity.posY < posY + 4 && entity.posY > posY - 4)
                                    {
                                        // Check EntityBomb
                                        if (entity is EntityBomb)
                                        {
                                            // If bomb explode
                                            ((EntityBomb)entity).executeAITask();
                                        }
                                        else if (entity is EntityPlayer)
                                        {
                                            // If Player setDead
                                            ((EntityPlayer)entity).setDead();
                                        }
                                        else
                                        {
                                            // Else setDead
                                            entity.setDead();
                                        }
                                    }
                                }

                                // Add every block to list
                                blocks.Add(World.terrain[posX + x, posY + y]);

                                // Change Terrain
                                World.changeTerrainElement(posX + x, posY + y, null);
                            }
                        }
                    }
                }
            }

            new Thread(Remake).Start();
        }

        private bool IsOutOfMap(int x, int y)
        {
            int newPosX = (int)(posX + x);
            int newPosY = (int)(posY + y);

            if (!(newPosX >= 0 && newPosX < World.terrain.GetLength(0)
                && newPosY > 0 && newPosY < 29))
            {
                if (!(newPosX < World.terrain.GetLength(0) - 1))
                {                       
                    return false;  
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Particle(int strength, explosiontype type, int x, int y)
        {

        }

        private void Remake()
        {
            // Start remake after 2 seconds
            Thread.Sleep(2 * 1000);

            // Start every block, from bottom to up
            for(int i = blocks.Count-1; i > 0; i--)
            {
                // Check if block isn't null
                if(blocks[i] != null)
                {
                    // Set element to blocks[i]
                    TerrainElement element = blocks[i];
                    // TerrainElement which will placed later
                    TerrainElement willadded;

                    // Check if TerrainElement
                    if(element is TerrainSolid)
                    {
                        willadded = new TerrainSolid(element.getX(), element.getY());
                    }
                    else if(element is TerrainSpike)
                    {
                        willadded = new TerrainSpike(element.getX(), element.getY());
                    }
                    else
                    {
                        willadded = new TerrainSolid(element.getX(), element.getY());
                    }

                    lock(WorldThread.WantToRender)
                    {
                        // Set willadded to wantToRender, for Render in WorldThread
                        WorldThread.WantToRender.Add(willadded);
                    }

                    lock(World.entities)
                    {
                        // Go every entity
                        foreach (Entity entity in World.entities)
                        {
                            // Check if entity is in radius
                            if (element.getX() < posX + 1 && element.getX() > posX - 1 && element.getY() < posY + 1 && element.getY() > posY - 1)
                            {
                                if (entity is EntityPlayer)
                                {
                                    ((EntityPlayer)entity).setDead();
                                }
                                else
                                {
                                    entity.setDead();
                                }
                            }
                        }
                    }

                    // Sleep after block setting
                    Thread.Sleep(100);
                }
            }

            // Spawn two new bombs
            for (int i = 0; i < 2; i++) {
                int random = World.rand.Next(110) + 4;
                new SpawnEntity(new EntityBomb(random, 2));
            }
        }
    }
}
