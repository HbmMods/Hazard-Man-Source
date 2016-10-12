using System;

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
