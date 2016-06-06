using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class WorldGenerator
    {

        public static int generationHeight = Console.WindowHeight / 2;
        public static int generationMax = Console.WindowHeight / 4 * 3;
        public static int generationMin = Console.WindowHeight / 4;

        public static void createNewWorld()
        {
            Random rand = new Random();
            generationHeight = Console.WindowHeight / 2;

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                if(i >= generationHeight)
                    World.terrain[0, i] = new TerrainSolid(0, i);

                if (i >= generationHeight)
                    World.terrain[1, i] = new TerrainSolid(1, i);

                if (i >= generationHeight)
                    World.terrain[2, i] = new TerrainSolid(2, i);
            }

            for (int i = 3; i < Console.WindowWidth; i++)
            {
                if(rand.Next(2) == 0)
                {
                    int z = rand.Next(2);

                    if (z == 0 && generationHeight < generationMax)
                    {
                        generationHeight += 1;
                    }
                    if (z == 1 && generationHeight > generationMin)
                    {
                        generationHeight -= 1;
                    }
                }

                for (int j = 0; j < Console.WindowHeight; j++)
                {
                    if (j >= generationHeight)
                        World.terrain[i, j] = new TerrainSolid(i, j);
                    else
                        World.terrain[i, j] = null;
                }
            }
        }

        public static void addSpike(int x)
        {
            for (int i = 1; i < World.terrain.GetLength(1) - 1; i++)
            {
                if(World.terrain[x, i] == null && World.terrain[x, i + 1] != null)
                {
                    World.terrain[x, i] = new TerrainSpike(x, i);
                    break;
                }
            }
        }
    }
}
