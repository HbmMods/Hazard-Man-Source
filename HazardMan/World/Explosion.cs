using System.Collections.Generic;
using System.Threading;

namespace HazardMan
{
    class Explosion
    {
        public enum explosiontype { ExplosionGeneric };
        public explosiontype type;
        public int strength;
        private int posX;
        private int posY;

        private List<TerrainElement> blocks = new List<TerrainElement>();

        public int getX()
        {
            return posX;
        }

        public void setX(int posX)
        {
            this.posX = posX;
        }

        public int getY()
        {
            return posY;
        }

        public void setY(int posY)
        {
            this.posY = posY;
        }

        public Explosion(int strength, explosiontype type, int posX, int posY)
        {
            this.strength = strength;
            this.type = type;
            this.setX(posX);
            this.setY(posY);
        }

        public void Damage(int strength, explosiontype type)
        {

        }

        public void Destroy(int radius, explosiontype type)
        {
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
                            if (World.terrain[getX() + x, getY() + y] != null)
                            {
                                // Check all entiy
                                foreach (Entity entity in World.entities)
                                {
                                    // Is in radius
                                    if (entity.getX() < getX() + 4 && entity.getX() > getX() - 4 && entity.getY() < getY() + 4 && entity.getY() > getY() - 4)
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
                                blocks.Add(World.terrain[getX() + x, getY() + y]);

                                // Change Terrain
                                World.changeTerrainElement(getX() + x, getY() + y, null);
                            }
                        }
                    }
                }
            }

            new Thread(Remake).Start();
        }

        private bool IsOutOfMap(int x, int y)
        {
            int newPosX = getX() + x;
            int newPosY = getY() + y;

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
                            if (entity.getX() < getX() + 5 && entity.getX() > getX() - 5 && entity.getY() < getY() + 5 && entity.getY() > getY() - 5)
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
                if (random < 10)
                {
                    i--;
                }
                else
                {
                    new SpawnEntity(new EntityBomb(random, 2));
                }
            }
        }
    }
}
