using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
