using System;

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
