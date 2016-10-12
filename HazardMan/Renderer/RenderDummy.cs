using System;

namespace HazardMan
{
    class RenderDummy : RenderEntity
    {
        public RenderDummy()
        {
            setColor(ConsoleColor.Black);
            setRenderIcon('X');
        }
    }
}
