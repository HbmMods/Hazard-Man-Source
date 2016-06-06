using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderBomb : RenderEntity
    {
        public RenderBomb()
        {
            setColor(ConsoleColor.Black);
            setRenderIcon('Ö');
        }
    }
}
