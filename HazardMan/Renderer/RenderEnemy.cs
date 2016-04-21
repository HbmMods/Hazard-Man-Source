using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan.Renderer
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
