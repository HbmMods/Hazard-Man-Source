using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class TerrainElement
    {
        public char[] renderIcon = {'#'};
        public ConsoleColor color = ConsoleColor.Gray;
        public ConsoleColor bg = ConsoleColor.Black;
        public int i = 0;
        public int j = 0;

        public void RenderSelf()
        {
            ConsoleColor c1 = Console.ForegroundColor;
            ConsoleColor c2 = Console.BackgroundColor;

            Console.ForegroundColor = color;
            Console.BackgroundColor = bg;
            
            Console.Write(renderIcon[World.rand.Next(renderIcon.Length)]);

            Console.ForegroundColor = c1;
            Console.BackgroundColor = c2;
        }
    }
}
