using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HazardMan
{
    class AIThread
    {
        private Thread thread;
        private bool running;

        public AIThread()
        {
            this.thread = new Thread(run);
        }

        public void start()
        {
            this.running = true;
            if (running)
                this.thread.Start();
        }

        public void stop()
        {
            this.running = false;
            this.thread.Abort();
        }

        private void run()
        {
            while (running)
            {
                lock (World.entities)
                {
                    foreach (Entity entity in World.entities)
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
                }

                Thread.Sleep(50);
            }
        }
    }
}
