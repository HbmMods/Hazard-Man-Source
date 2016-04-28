using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderEnemy : RenderEntity
    {
        public RenderEnemy()
        {
            fg = ConsoleColor.DarkRed;
            renderIcon = '#';
        }
    }
}
