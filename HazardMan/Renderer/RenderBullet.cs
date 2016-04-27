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
            fg = ConsoleColor.Red;
            //renderIcon = '¤';
            renderIcon = '■';
        }
    }
}
