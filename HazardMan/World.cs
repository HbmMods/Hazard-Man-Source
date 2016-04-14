using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class World
    {
        public static Random rand = new Random();
        public static List<Entity> entities = new List<Entity>();
        public static List<Entity> kill = new List<Entity>();
        public static Thread updateTicks = new Thread(UpdateWorld);
        public static Thread keyInput = new Thread(Input);
        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];
        public volatile static bool tickWorld = false;
        public static ConsoleKey input;

        public static void StartWorld()
        {
            //entities.Add(new EntityDummy(Console.WindowWidth / 2, Console.WindowHeight / 2));
            //entities.Add(new EntityDummy(0, Console.WindowHeight / 2 - 5));
            entities.Add(new EntityDummy(Console.WindowWidth / 3, 2, ConsoleKey.W, ConsoleKey.A, ConsoleKey.D));
            entities.Add(new EntityDummy(Console.WindowWidth / 2, 2, ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow));
            
            tickWorld = true;
            Console.Clear();
            WorldGenerator.createNewWorld();
            Renderer.RenderWorld();
            Console.BackgroundColor = ConsoleColor.Blue;

            updateTicks.Start();
            keyInput.Start();
        }

        public static void UpdateWorld()
        {
            while (tickWorld)
            {
                if (!tickWorld)
                {
                    StopWorld();
                    return;
                }

                foreach (Entity e in kill)
                {
                    e.renderer.delRenderEntity();
                    entities.Remove(e);
                }
                
                kill = new List<Entity>();

                foreach (Entity e in entities)
                {
                    e.Update();
                }

                Thread.Sleep(50);
            }
        }

        public static void Input()
        {
            while(true)
            {
                input = Console.ReadKey(true).Key;
            }
        }

        public static void StopWorld()
        {
            updateTicks.Abort();
        }
    }
}
