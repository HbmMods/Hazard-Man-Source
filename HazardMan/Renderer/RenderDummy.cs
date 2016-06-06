using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderDummy : RenderEntity
    {
        public RenderDummy()
        {
            setColor(ConsoleColor.Black);
            setRenderIcon('X');
        }
    }
}
