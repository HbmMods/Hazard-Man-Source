using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class WorldThread
    {
        private Thread thread;

        public static List<Entity> wantToDie = new List<Entity>();

        public WorldThread()
        {
            this.thread = new Thread(run);
        }

        public void start()
        {
            this.thread.Start();
        }

        public void stop()
        {

            this.thread.Abort();
        }

        public void run()
        {
            while (World.tickWorld)
            {
                if (!World.tickWorld)
                {
                    World.StopWorld();
                    break;

                }

                lock (World.entities)
                {
                    foreach (Entity entity in World.entities)
                    {
                        entity.Update();
                    }

                    foreach (Entity entity in wantToDie)
                    {
                        entity.renderer.delete();
                        World.entities.Remove(entity);
                    }

                    wantToDie = new List<Entity>();
                }

                scoreUpdate();

                try { Thread.Sleep(50); } catch { }
            }
        }

        public static void scoreUpdate()
        {
            int i = 0;

            foreach (OptionPlayer player in Library.players)
            {
                EntityPlayer eplayer = null;
                lock (World.entities)
                {
                    foreach (Entity entity in World.entities)
                    {
                        if (entity is EntityPlayer)
                        {
                            if (player.getName().Contains(((EntityPlayer)entity).getName()))
                            {
                                eplayer = (EntityPlayer)entity;
                                break;
                            }
                        }
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                if (i == 0) Console.SetCursorPosition(0, 29);
                else if (i == 1) Console.SetCursorPosition(30, 29);
                else if (i == 2) Console.SetCursorPosition(60, 29);
                else if (i == 3) Console.SetCursorPosition(90, 29);
                Console.ForegroundColor = player.getColor();
                Console.Write(player.getName() + " - " + Library.score[player] + " ");

                for (int y = 0; y < eplayer.getHealth(); y++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♥ ");
                }

                for (int y = 0; y < eplayer.getMaxHealth(); y++)
                {
                    Console.Write("  ");
                }

                i++;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(110, 29);
            Console.Write("Level: " + Library.getLevel());

        }
    }
}
