using System;

namespace HazardMan
{
    class RenderEnemy : RenderEntity
    {
        public RenderEnemy()
        {
            setColor(ConsoleColor.DarkRed);
            setRenderIcon('#');
        }
    }
}
