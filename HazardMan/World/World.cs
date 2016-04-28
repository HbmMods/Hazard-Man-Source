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

            foreach(OptionPlayer player in Library.players) {
                spawnEntity(new EntityPlayer(1, Console.WindowHeight / 2 - 1, player.getUpKey(), player.getLeftKey(), player.getRightKey(), player.getColor(), player.getName()));
            }

            switch(rand.Next(3))
            {
                case 0:
                    entities.Add(new EntityEnemy(Console.WindowWidth / 5 * 1, 2));
                    entities.Add(new EntityEnemy(Console.WindowWidth / 5 * 2, 2));
                    entities.Add(new EntityEnemy(Console.WindowWidth / 5 * 3, 2));
                    entities.Add(new EntityEnemy(Console.WindowWidth / 5 * 4, 2));
                    break;
                case 1:
                    entities.Add(new EntityShooter(Console.WindowWidth / 5 * 1, 2));
                    entities.Add(new EntityShooter(Console.WindowWidth / 5 * 2, 2));
                    entities.Add(new EntityShooter(Console.WindowWidth / 5 * 3, 2));
                    entities.Add(new EntityShooter(Console.WindowWidth / 5 * 4, 2));
                    break;
                case 2:
                    entities.Add(new EntitySprayer(Console.WindowWidth / 5 * 1, 2));
                    entities.Add(new EntitySprayer(Console.WindowWidth / 5 * 2, 2));
                    entities.Add(new EntitySprayer(Console.WindowWidth / 5 * 3, 2));
                    entities.Add(new EntitySprayer(Console.WindowWidth / 5 * 4, 2));
                    break;
            }


            foreach (OptionPlayer player in Library.players)
            {
                if (!Library.score.ContainsKey(player))
                    Library.score.Add(player, 0);
            }

            tickWorld = true;
            Console.Clear();
            WorldGenerator.createNewWorld();
            WorldGenerator.addSpike(20);
            WorldGenerator.addSpike(21);
            WorldGenerator.addSpike(22);
            WorldGenerator.addSpike(46);
            WorldGenerator.addSpike(47);
            WorldGenerator.addSpike(63);
            WorldGenerator.addSpike(64);
            WorldGenerator.addSpike(87);
            WorldGenerator.addSpike(88);
            WorldGenerator.addSpike(89);
            WorldGenerator.addSpike(110);
            WorldGenerator.addSpike(111);
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

                try {
                    foreach (Entity entity in kill)
                    {
                        entity.renderer.delRenderEntity();
                        entities.Remove(entity);
                    }
                } catch { }
                
                kill = new List<Entity>();

                while (blockThread) { }

                try {
                    foreach (Entity entity in entities)
                    {
                        if (blockThread)
                            break;
                        entity.Update();
                    }
                } catch { }

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
                    Console.ForegroundColor = player.getColor();
                    Console.Write(player.getName() + " - " + Library.score[player]);
                    i++;
                }
            }
            catch { }
        }

        public static void StopWorld()
        {
            updateTicks.Abort();
            keyInput.Abort();
            mobAI.Abort();

            entities.Clear();
            Library.score.Clear();
            kill.Clear();
        }

        public static bool spawnEntity(Entity entity)
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
