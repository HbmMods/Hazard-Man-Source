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

        public static Thread updateTicks;
        public static Thread keyInput;
        public static Thread mobAI;

        public static Dictionary<uint, uint> score;

        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];

        public volatile static bool tickWorld = false;

        public static ConsoleKey input;

        public static void StartWorld()
        {
            updateTicks = new Thread(UpdateWorld);
            keyInput = new Thread(Input);
            mobAI = new Thread(AI);

            uint id = 0;
            foreach(Option_Player player in Option.players) {
                entities.Add(new EntityPlayer(Console.WindowWidth / 3, 2, player.getUpKey(), player.getLeftKey(), player.getRightKey(), id++));
            }
            entities.Add(new EntityEnemy(Console.WindowWidth / 3 + 10, 2));

            score = new Dictionary<uint, uint>();
            score.Clear();

            foreach(Entity entity in entities)
            {
                if (entity is EntityPlayer)
                {
                    EntityPlayer player = (EntityPlayer)entity;
                    if(!score.ContainsKey(player.id))
                        score.Add(player.id, 0);
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

                foreach (Entity entity in kill)
                {
                    entity.renderer.delRenderEntity();
                    entities.Remove(entity);
                }
                
                kill = new List<Entity>();

                foreach (Entity entity in entities)
                {
                    entity.Update();
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
                    foreach (Entity entity in World.entities)
                    {
                        entity.setDeath();
                    }

                    tickWorld = false;
                }

                Thread.Sleep(25);
            }
        }

        public static void AI()
        {
            while (tickWorld)
            {
                foreach (Entity entity in entities)
                {
                    if(entity is EntityAI)
                    {
                        EntityAI entitiyAI = (EntityAI)entity;

                        if(entitiyAI.executeAICheck())
                        {
                            entitiyAI.executeAITask();
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
