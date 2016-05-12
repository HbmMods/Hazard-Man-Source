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

        public static List<Entity> entities;

        public static TerrainElement[,] terrain = new TerrainElement[Console.WindowWidth, Console.WindowHeight];

        public volatile static bool tickWorld = false;

        public static ConsoleKey input;

        private static List<int> alreadyspawned = new List<int>();

        public static void StartWorld()
        {
            arrayInit();

            entities = new List<Entity>();

            Library.worldThread = new WorldThread();
            Library.inputThread = new InputThread();
            Library.aiThread = new AIThread();

            foreach(OptionPlayer player in Library.players) {
                new SpawnEntity(new EntityPlayer(1, Console.WindowHeight / 2 - 1, player.getUpKey(), player.getLeftKey(), player.getRightKey(), player.getColor(), player.getName()));

                if (!Library.score.ContainsKey(player))
                    Library.score.Add(player, 0);
            }

            tickWorld = true;
            Console.Clear();
            WorldGenerator.createNewWorld();

            spawnAtStart();

            Renderer.RenderWorld();
            Console.BackgroundColor = ConsoleColor.Blue;

            Library.worldThread.start();
            Library.inputThread.start();
            Library.aiThread.start();


        }

        public static void StopWorld()
        {
            World.tickWorld = false;

            Library.worldThread.stop();
            Library.inputThread.stop();
            Library.aiThread.stop();

            Library.inputThread = null;
            Library.aiThread = null;
            Library.worldThread = null;

            entities.Clear();
            Library.score.Clear();
            Console.Clear();
        }

        public static void spawnAtStart()
        {
            int forrun = Library.getLevel() * 2;
            for (int i = 0; i < forrun; i++)
            {
                int random = rand.Next(110) + 4;
                if (alreadyspawned.Contains(random))
                {
                    i--;
                }
                else
                {
                    alreadyspawned.Add(random);
                    WorldGenerator.addSpike(random);
                }
            }

            for (int i = 0; i < Library.getLevel(); i++)
            {
                int random = rand.Next(110) + 4;
                if (alreadyspawned.Contains(random))
                {
                    i--;
                }
                else
                {
                    alreadyspawned.Add(random);

                    switch (rand.Next(4))
                    {
                        case 0:
                            new SpawnEntity(new EntityEnemy(random, 2));
                            break;
                        case 1:
                            new SpawnEntity(new EntityShooter(random, 2));
                            break;
                        case 2:
                            new SpawnEntity(new EntitySprayer(random, 2));
                            break;
                        case 3:
                            new SpawnEntity(new EntityBomb(random, 2));
                            break;
                    }
                }
            } 
        }

        public static void changeTerrainElement(int x, int y, TerrainElement element)
        {
            try {
                terrain[x, y] = element;
                Renderer.rerenderAtPos(x, y);
            } catch { }
        }

        //--------------------Critical Section Manager------------------

        public static int process_wt = 0;
        public static int process_se = 1;
        public static int process_de = 2;
        public static int process_ait = 3;
        public static int process_ef = 4;
        public static int process_eb = 5;
        public static int process_ee = 6;

        static int threads = 7;
        static int[] ticket = new int[threads];
        static bool[] entering = new bool[threads];

        public static void doLock(int pid)
        {
            for (int i = 0; i < threads; i++)
            {
                ticket[i] = 0;
                entering[i] = false;
            }
            entering[pid] = true;

            int max = 0;

            for (int i = 0; i < threads; i++)
            {
                if (ticket[i] > ticket[max]) { max = i; }
            }

            ticket[pid] = 1 + max;
            entering[pid] = false;


            for (int i = 0; i < threads; ++i)
            {
                if (i != pid)
                {
                    while (entering[i])
                    {
                        Thread.Yield();
                    }
                    while (ticket[i] != 0 && (ticket[pid] > ticket[i] ||
                              (ticket[pid] == ticket[i] && pid > i)))
                    {
                        Thread.Yield();
                    }
                }
            }
        }

        public static void unlock(int pid)
        {
            ticket[pid] = 0;
        }

        public static void arrayInit()
        {
            for (int i = 0; i < threads; i++)
            {
                ticket[i] = 0;
                entering[i] = false;
            }
        }
    }
}
