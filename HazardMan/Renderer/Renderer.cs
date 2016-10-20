using System;

namespace HazardMan
{
    class Renderer
    {
        public static void renderWorld()
        {
            for (int j = 0; j < Console.WindowHeight - 1; j++)
            {
                for (int i = 0; i < Console.WindowWidth; i++)
                {
                    if (World.terrain[i, j] != null)
                    {
                        World.terrain[i, j].renderElement();
                    }
                    else
                    {
                        renderSky();
                    }
                }
            }
        }

        public static void rerenderAtPos(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            if (World.terrain[i, j] != null)
            {
                World.terrain[i, j].renderElement();
            }
            else
            {
                renderSky();
            }
        }

        public static void renderSky()
        {
            ConsoleColor foregroundcolor = Console.ForegroundColor;
            ConsoleColor backgroundcolor = Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = getSkyColor();

            Console.Write(" ");

            Console.ForegroundColor = foregroundcolor;
            Console.BackgroundColor = backgroundcolor;

        }

        public static ConsoleColor getSkyColor()
        {
            return ConsoleColor.Blue;
        }
    }
}
