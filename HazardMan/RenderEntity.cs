using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    abstract class RenderEntity
    {
        public int renderPosX;
        public int renderPosY;
        public char renderIcon = '?';
        public ConsoleColor bg = ConsoleColor.DarkRed;

        public void renderEntityAt(Entity entity)
        {
            renderPosX = (int)entity.posX;
            renderPosY = (int)entity.posY;

            ConsoleColor c1 = Console.ForegroundColor;

            Console.ForegroundColor = bg;

            if (entity.posX > -1 && entity.posX < 120 && entity.posY > -1 && entity.posY < 30)
            {
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;
                Console.Write(renderIcon);

                if (!((int)entity.lastPosX == (int)entity.posX && (int)entity.lastPosY == (int)entity.posY))
                {
                    if(World.terrain[(int)entity.lastPosX, (int)entity.lastPosY] != null)
                    {
                        Console.CursorLeft = (int)entity.lastPosX;
                        Console.CursorTop = (int)entity.lastPosY;
                        World.terrain[(int)entity.lastPosX, (int)entity.lastPosY].RenderSelf();
                    }
                    else
                    {
                        Console.CursorLeft = (int)entity.lastPosX;
                        Console.CursorTop = (int)entity.lastPosY;
                        Renderer.RenderSky();
                    }
                }
            }

            Console.ForegroundColor = c1;
        }

        public void delRenderEntity()
        {
            if (World.terrain[renderPosX, renderPosY] != null)
            {
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;
                World.terrain[renderPosX, renderPosY].RenderSelf();
            }
            else
            {
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;
                Renderer.RenderSky();
            }
        }
    }
}
