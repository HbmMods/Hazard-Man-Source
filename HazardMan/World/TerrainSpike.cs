using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class TerrainSpike : TerrainElement
    {
        public TerrainSpike()
        {
            renderIcon = new char[] { 'I' };
            color = ConsoleColor.Gray;
            bg = ConsoleColor.Red;
        }
    }
}
