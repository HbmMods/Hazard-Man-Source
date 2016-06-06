using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderEntitySmokeFX : RenderEntity
    {
        public RenderEntitySmokeFX()
        {
            setColor(ConsoleColor.Gray);
            setRenderIcon('█');
        }
    }
}
