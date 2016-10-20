using System;

namespace HazardMan
{
    class TerrainSpike : TerrainElement
    {
        public TerrainSpike(int x, int y) : base (x, y)
        {
            renderIcon = new char[] { '▲' };
            setColor(ConsoleColor.DarkGray);
            setBackGroundColor(ConsoleColor.Blue);
        }
    }
}
