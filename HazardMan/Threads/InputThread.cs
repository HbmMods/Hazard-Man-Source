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
        private bool running;

        public InputThread()
        {
            this.thread = new Thread(run);
        }

        public void start()
        {
            running = true;
            if (running)
                this.thread.Start();
        }

        public void stop()
        {
            running = false;
            this.thread.Abort();
        }

        public void run()
        {
            while(running)
            {
                World.input = Console.ReadKey(true).Key;

                if (World.input == ConsoleKey.Escape)
                {
                    World.StopWorld();
                }

                Thread.Sleep(10);
            }
        }
    }
}
