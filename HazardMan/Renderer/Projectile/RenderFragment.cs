using System;

namespace HazardMan
{
    class RenderFragment : RenderEntity
    {
        public RenderFragment()
        {
            setColor(ConsoleColor.Black);
            setRenderIcon('¤');
            //renderIcon = '■';
        }
    }
}
