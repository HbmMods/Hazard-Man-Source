using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class RenderFragment : RenderEntity
    {
        public RenderFragment()
        {
            fg = ConsoleColor.Black;
            renderIcon = '¤';
            //renderIcon = '■';
        }
    }
}
