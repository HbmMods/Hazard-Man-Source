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
        private char renderIcon = '?';
        private ConsoleColor color = ConsoleColor.DarkRed;

        public void renderEntityAt(Entity entity)
        {
            // Set render positions
            renderPosX = (int)entity.getX();
            renderPosY = (int)entity.getY();

            // Set fore and background colors
            ConsoleColor foregroundcolor = Console.ForegroundColor;
            ConsoleColor backgroundcolor = Console.BackgroundColor;

            // Get color of this entityw
            Console.ForegroundColor = getColor();

            // Check if terrain isn't null on render position
            if (World.terrain[renderPosX, renderPosY] != null)
            {
                // Set background color of terrainelement background color
                Console.BackgroundColor = World.terrain[renderPosX, renderPosY].getBackGroundColor();
            }
            else
            {
                // Set background color of sky
                Console.BackgroundColor = Renderer.getSkyColor();
            }

            // Set postions and last positions
            int lastPosX = (int)entity.lastPosX;
            int lastPosY = (int)entity.lastPosY;
            int posX = (int)entity.getX();
            int posY = (int)entity.getY();

            // Check if position is in console
            if (posX > -1 && posY < 120 && entity.getY() > -1 && entity.getY() < 30)
            {
                // Set Cursor to render postions
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;

                // Write renderIcon
                Console.Write(renderIcon);

                // Check if lastPos changed, if changed rerender
                if (!(lastPosX == posX && lastPosY == posY))
                {
                    // Check if terrainelement isn't null
                    if (World.terrain[lastPosX, lastPosY] != null)
                    {
                        // Set Cursor to last position
                        Console.CursorLeft = lastPosX;
                        Console.CursorTop = lastPosY;
                        World.terrain[lastPosX, lastPosY].renderElement();
                    }
                    else
                    {
                        // Render sky if terrainelement is null
                        Console.CursorLeft = lastPosX;
                        Console.CursorTop = lastPosY;
                        Renderer.renderSky();
                    }
                }
            }

            // Reset foreground and background
            Console.ForegroundColor = foregroundcolor;
            Console.BackgroundColor = backgroundcolor;
        }

        public void delete()
        {
            // Delete renderer

            // Check if terrain isn't null
            if (World.terrain[renderPosX, renderPosY] != null)
            {
                // Set cursor positions
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;

                // Render element
                World.terrain[renderPosX, renderPosY].renderElement();
            }
            else
            {
                // Set cursor positons
                Console.CursorLeft = renderPosX;
                Console.CursorTop = renderPosY;

                // Render sky
                Renderer.renderSky();
            }
        }

        public ConsoleColor getColor()
        {
            return color;
        }

        public void setColor(ConsoleColor color)
        {
            this.color = color;
        }

        public void setRenderIcon(char icon)
        {
            this.renderIcon = icon;
        }
    }
}
