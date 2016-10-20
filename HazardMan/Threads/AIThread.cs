using System.Threading;

namespace HazardMan
{
    class AIThread
    {
        public static int tick = 50;

        private Thread thread;

        public AIThread()
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

        private void run()
        {
            while (World.tickWorld)
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

                try { Thread.Sleep(tick); }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
