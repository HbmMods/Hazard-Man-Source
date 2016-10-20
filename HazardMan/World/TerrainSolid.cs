using System;

namespace HazardMan
{
    class TerrainSolid : TerrainElement
    {
        public TerrainSolid(int x, int y) : base (x, y)
        {
            renderIcon = new char[]{ '*', ',', '.', '´', '`', '"' };
            setColor(ConsoleColor.Gray);
            setBackGroundColor(ConsoleColor.DarkGray);
        }
    }
}
