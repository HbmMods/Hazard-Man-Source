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
            this.thread.Interrupt();
        }

        private void run()
        {
            while(running)
            {
                try
                {
                    World.doLock(World.process_ait);

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

                    World.unlock(World.process_ait);
                }
                catch { }

                try { Thread.Sleep(50); } catch { }
            }
        }
    }
}
