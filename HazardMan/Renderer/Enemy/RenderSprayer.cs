using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderSprayer : RenderEntity
    {
        public RenderSprayer()
        {
            setColor(ConsoleColor.DarkMagenta);
            setRenderIcon('▓');
        }
    }
}
