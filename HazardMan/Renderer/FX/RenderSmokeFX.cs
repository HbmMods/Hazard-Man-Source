using System;

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
