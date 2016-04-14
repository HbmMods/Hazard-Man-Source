using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class TerrainSolid : TerrainElement
    {
        public TerrainSolid()
        {
            renderIcon = new char[]{ '*', ',', '.', '´', '`', '"' };
            color = ConsoleColor.Gray;
            bg = ConsoleColor.DarkGray;
        }
    }
}
