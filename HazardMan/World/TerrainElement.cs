using System;

namespace HazardMan
{
    class TerrainElement
    {
        public char[] renderIcon = {'#'};
        private ConsoleColor color = ConsoleColor.Gray;
        private ConsoleColor background = ConsoleColor.Black;

        private int x;
        private int y;

        public TerrainElement(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void renderElement()
        {
            ConsoleColor foregroundcolor = Console.ForegroundColor;
            ConsoleColor backgroundcolor = Console.BackgroundColor;

            Console.ForegroundColor = color;
            Console.BackgroundColor = background;
            
            Console.Write(renderIcon[World.rand.Next(renderIcon.Length)]);

            Console.ForegroundColor = foregroundcolor;
            Console.BackgroundColor = backgroundcolor;
        }

        public ConsoleColor getColor()
        {
            return color;
        }

        public ConsoleColor getBackGroundColor()
        {
            return background;
        }

        public void setColor(ConsoleColor color)
        {
            this.color = color;
        }

        public void setBackGroundColor(ConsoleColor color)
        {
            this.background = color;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }
}
