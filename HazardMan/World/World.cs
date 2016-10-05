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

        public static void startWorld()
        {
            entities = new List<Entity>();

            Library.worldThread = new WorldThread();
            Library.inputThread = new InputThread();
            Library.aiThread = new AIThread();

            int startleft = 1;
            foreach(OptionPlayer player in Library.players) {
                new SpawnEntity(new EntityPlayer(startleft, Console.WindowHeight / 2 - 10, player.getUpKey(), player.getLeftKey(), player.getRightKey(), player.getColor(), player.getName()));

                if (!Library.score.ContainsKey(player))
                    Library.score.Add(player, 0);

                startleft++;
            }

            tickWorld = true;
            Console.Clear();
            WorldGenerator.createNewWorld();

            spawnAtStart();

            Renderer.renderWorld();
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

            lock(entities)
            {
                entities.Clear();
            }
            Console.Clear();
        }

        public static void spawnAtStart()
        {
            // Set how many sparks will spawn
            int forrun = Library.getLevel() * 2;

            // Set masterzaehler
            int masterzaehler = 0;

            // Create spikes
            for (int i = 0; i < forrun; i++)
            {
                int random = rand.Next(110) + 4;

                // Check if spike already spawned at this position
                if (alreadyspawned.Contains(random))
                {
                    i--;
                }
                else
                {
                    // Create Spike
                    alreadyspawned.Add(random);
                    WorldGenerator.addSpike(random);
                }

                // Add to masterzaehler 1
                masterzaehler++;

                // IF masterzaehler is to high, break
                if (masterzaehler > 60)
                    break;
                
            }

            // Reset masterzaehler
            masterzaehler = 0;

            // Create Enemys
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

                if (masterzaehler > 60)
                    break;
            } 
        }

        public static void changeTerrainElement(int x, int y, TerrainElement element)
        {
            try {
                terrain[x, y] = element;
                Renderer.rerenderAtPos(x, y);
            } catch { }
        }       
    }
}
