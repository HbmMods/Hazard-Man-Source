using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderBullet : RenderEntity
    {
        public RenderBullet()
        {
            setColor(ConsoleColor.Black);
            //renderIcon = '¤';
            //renderIcon = '■';
            setRenderIcon('─');
        }
    }
}
