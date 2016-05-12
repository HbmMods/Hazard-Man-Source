using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Renderer
    {
        public static void RenderWorld()
        {
            for (int j = 0; j < Console.WindowHeight - 1; j++)
            {
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    if (World.terrain[i, j] != null)
                    {
                        World.terrain[i, j].RenderSelf();
                    }
                    else
                    {
                        RenderSky();
                    }
                }
            }
        }

        public static void rerenderAtPos(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            if (World.terrain[i, j] != null)
            {
                World.terrain[i, j].RenderSelf();
            }
            else
            {
                RenderSky();
            }
        }

        public static void RenderSky()
        {
            ConsoleColor c1 = Console.ForegroundColor;
            ConsoleColor c2 = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Blue;

            Console.Write(" ");

            Console.ForegroundColor = c1;
            Console.BackgroundColor = c2;

        }
    }
}
