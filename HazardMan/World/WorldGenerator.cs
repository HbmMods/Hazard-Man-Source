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


            for (int i = 0; i < Console.WindowHeight; i++)
            {
                if(i >= generationHeight)
                    World.terrain[0, i] = new TerrainSolid();

                if (i >= generationHeight)
                    World.terrain[1, i] = new TerrainSolid();

                if (i >= generationHeight)
                    World.terrain[2, i] = new TerrainSolid();
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
                        World.terrain[i, j] = new TerrainSolid();
                }
            }
        }

        public static void scrollExistingWolrd()
        {

        }
    }
}
