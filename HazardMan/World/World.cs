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
        public static bool blockThread = false;

        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];

        public volatile static bool tickWorld = false;

        public static ConsoleKey input;

        public static void StartWorld()
        {
            updateTicks = new Thread(UpdateWorld);
            keyInput = new Thread(Input);
            mobAI = new Thread(AI);

            int id = 0;
            foreach(OptionPlayer player in Library.players) {
                entities.Add(new EntityPlayer(Console.WindowWidth / 3, 2, player.getUpKey(), player.getLeftKey(), player.getRightKey(), id++, player.getColor(), player.getName()));
            }
            entities.Add(new EntityEnemy(Console.WindowWidth / 3 + 10, 2));
            entities.Add(new EntityShooter(Console.WindowWidth / 3 + 15, 2));

            Library.score = new Dictionary<OptionPlayer, int>();
            Library.score.Clear();

            foreach (OptionPlayer player in Library.players)
            {
                if (!Library.score.ContainsKey(player))
                    Library.score.Add(player, 0);
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

                while (blockThread) { }

                foreach (Entity entity in entities)
                {
                    if (blockThread)
                        break;
                    entity.Update();
                }

                scoreUpdate();

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
                        entity.setDead();
                    }

                    tickWorld = false;
                }

                Thread.Sleep(50);
            }
        }

        public static void AI()
        {
            while (tickWorld)
            {
                try {
                    foreach (Entity entity in entities)
                    {
                        if (entity is EntityAI)
                        {
                            EntityAI entitiyAI = (EntityAI)entity;

                            if (entitiyAI.executeAICheck())
                            {
                                entitiyAI.executeAITask();
                            }
                        }
                    }
                } catch 
                {

                }

                Thread.Sleep(50);
            }
        }

        public static void scoreUpdate()
        {
            int i = 0;
            try
            {
                foreach (OptionPlayer player in Library.players)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    if (i == 0) Console.SetCursorPosition(0, 29);
                    else if (i == 1) Console.SetCursorPosition(20, 29);
                    else if (i == 2) Console.SetCursorPosition(40, 29);
                    else if (i == 3) Console.SetCursorPosition(60, 29);
                    Console.Write(player.getName() + " - " + Library.score[player]);
                    i++;
                }
            }
            catch { }
        }

        public static void StopWorld()
        {
            updateTicks.Interrupt();
            keyInput.Interrupt();
            mobAI.Interrupt();

            entities.Clear();
            Library.score.Clear();
            kill.Clear();
        }

        public static bool spawnEntityInWorld(Entity entity)
        {
            if (entity != null)
            {
                blockThread = true;
                Thread.Sleep(10);
                entities.Add(entity);
                blockThread = false;
                return true;
            }
            return false;
        }
    }
}
