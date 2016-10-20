using System;

namespace HazardMan
{
    class RenderShooter : RenderEntity
    {
        public RenderShooter()
        {
            setColor(ConsoleColor.Black);
            setRenderIcon('╦');
        }
    }
}
