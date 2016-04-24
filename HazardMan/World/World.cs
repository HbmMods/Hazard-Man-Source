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
        public static Thread mobAI = new Thread(AI);

        public static Dictionary<uint, uint> score;

        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];

        public volatile static bool tickWorld = false;

        public static ConsoleKey input;

        public static void StartWorld()
        {
            uint id = 0;
            entities.Add(new EntityPlayer(Console.WindowWidth / 3, 2, ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, id++));
            entities.Add(new EntityPlayer(Console.WindowWidth / 3 + 3, 2, ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, id++));
            entities.Add(new EntityEnemy(Console.WindowWidth / 3 + 10, 2));

            score = new Dictionary<uint, uint>();
            score.Clear();

            foreach(Entity p in entities)
            {
                if (p is EntityPlayer)
                {
                    if(!score.ContainsKey(((EntityPlayer)p).id))
                        score.Add(((EntityPlayer)p).id, 0);
                }
            }

            tickWorld = true;
            Console.Clear();
            WorldGenerator.createNewWorld();
            Renderer.RenderWorld();
            Console.BackgroundColor = ConsoleColor.Blue;

            updateTicks.Start();
            keyInput.Start();
            mobAI.Start();
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
            while(tickWorld)
            {
                input = Console.ReadKey(true).Key;

                if(input == ConsoleKey.Escape)
                {
                    tickWorld = false;
                }

                Thread.Sleep(25);
            }
        }

        public static void AI()
        {
            while (tickWorld)
            {
                foreach (Entity e in entities)
                {
                    if(e is EntityAI)
                    {
                        if(((EntityAI)e).executeAICheck())
                        {
                            ((EntityAI)e).executeAITask();
                        }
                    }
                }

                Thread.Sleep(200);
            }
        }

        public static void StopWorld()
        {
            updateTicks.Interrupt();
            keyInput.Interrupt();
            mobAI.Interrupt();
            entities.Clear();
            score.Clear();
            kill.Clear();
        }
    }
}
