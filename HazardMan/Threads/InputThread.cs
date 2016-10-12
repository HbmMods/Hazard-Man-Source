using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class InputThread
    {
        private Thread thread;

        public InputThread()
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
            while(World.tickWorld)
            {
                World.input = Console.ReadKey(true).Key;

                if (World.input == ConsoleKey.Escape)
                {
                    World.StopWorld();
                }
                else if (World.input == ConsoleKey.Spacebar)
                {
                    lock (World.entities)
                    {
                        foreach (Entity entity in World.entities)
                        {
                            if (entity is EntityPlayer) entity.setDead();
                        }
                    }
                }

                try { Thread.Sleep(10); }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
