using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
