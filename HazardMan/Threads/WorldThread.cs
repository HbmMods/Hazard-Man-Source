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
        private bool running;

        public static List<Entity> wantToDie = new List<Entity>();

        public WorldThread()
        {
            this.thread = new Thread(run);
        }

        public void start()
        {
            this.running = true;
            if (this.running)
                this.thread.Start();
        }

        public void stop()
        {
            this.running = false;
            this.thread.Interrupt();
        }

        public void run()
        {
            while(running)
            {
                if (!World.tickWorld)
                {
                    World.StopWorld();
                    return;
                }

                World.doLock(World.process_wt);

                try {
                    foreach (Entity entity in World.entities)
                    {
                        entity.Update();
                    }
                

                    foreach(Entity entity in wantToDie)
                    {
                        entity.renderer.delRenderEntity();
                        World.entities.Remove(entity);
                    }
                }
                catch { }

                World.unlock(World.process_wt);

                wantToDie = new List<Entity>();

                scoreUpdate();

                try { Thread.Sleep(50); } catch { }
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

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(110, 29);
                Console.Write("Level: " + Library.getLevel());
            }
            catch { }
        }
        
    }


}
