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
        public static Thread scoreUpdate;

        public static Dictionary<EntityPlayer, int> score;

        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];

        public volatile static bool tickWorld = false;

        public static ConsoleKey input;

        public static void StartWorld()
        {
            updateTicks = new Thread(UpdateWorld);
            keyInput = new Thread(Input);
            mobAI = new Thread(AI);
            scoreUpdate = new Thread(scoreUpdater);

            int id = 0;
            foreach(Option_Player player in Library.players) {
                entities.Add(new EntityPlayer(Console.WindowWidth / 3, 2, player.getUpKey(), player.getLeftKey(), player.getRightKey(), id++, player.getColor(), player.getName()));
            }
            entities.Add(new EntityEnemy(Console.WindowWidth / 3 + 10, 2));

            score = new Dictionary<EntityPlayer, int>();
            score.Clear();

            foreach(Entity entity in entities)
            {
                if (entity is EntityPlayer)
                {
                    EntityPlayer player = (EntityPlayer)entity;
                    if(!score.ContainsKey(player))
                        score.Add(player, 0);
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
            scoreUpdate.Start();
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
                        entity.setDead();
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
                try {
                    foreach (Entity entity in entities)
                    {
                        if (entity is EntityAI)
                        {
                            EntityAI entitiyAI = (EntityAI)entity;

                            if (entitiyAI.executeAICheck())
                            {
                                if (Library.isSoundActivated) Console.Beep(200, 200);
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

        public static void scoreUpdater()
        {
            /*while(tickWorld)
            {
                int space = 0;

                foreach (Entity entity in World.entities)
                {
                    if(entity is EntityPlayer)
                    {
                        EntityPlayer player = (EntityPlayer)entity;

                        Console.SetCursorPosition(space, 29);
                        Console.WriteLine(player.getName() + "Score: " + score[player]);
                    }
                }

                Thread.Sleep(1000);
            }*/
        }

        public static void StopWorld()
        {
            updateTicks.Interrupt();
            keyInput.Interrupt();
            mobAI.Interrupt();
            scoreUpdate.Interrupt();
            entities.Clear();
            score.Clear();
            kill.Clear();
        }
    }
}
