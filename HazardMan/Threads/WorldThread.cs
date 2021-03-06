﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace HazardMan
{
    class WorldThread
    {
        private readonly Thread thread;

        public static List<Entity> WantToDie = new List<Entity>();
        public static List<TerrainElement> WantToRender = new List<TerrainElement>();

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

                    foreach (Entity entity in WantToDie)
                    {
                        entity.renderer.delete();
                        World.entities.Remove(entity);
                    }

                    WantToDie = new List<Entity>();
                }

                lock(WantToRender)
                {
                    foreach (TerrainElement element in WantToRender)
                    {                   
                        if (element is TerrainSolid)
                        {
                            World.changeTerrainElement(element.getX(), element.getY(), ((TerrainSolid)element));
                        }
                        else if(element is TerrainSpike)
                        {
                            World.changeTerrainElement(element.getX(), element.getY(), ((TerrainSpike)element));
                        }
                    }

                    WantToRender = new List<TerrainElement>();
                }

                ScoreUpdate();

                try { Thread.Sleep(50); }
                catch
                {
                    // ignored
                }
            }
        }

        public static void ScoreUpdate()
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

                if(eplayer ==  null) return;

                Console.BackgroundColor = ConsoleColor.Black;
                if (i == 0) Console.SetCursorPosition(1, Console.WindowHeight - 1);
                else if (i == 1) Console.SetCursorPosition(31, Console.WindowHeight - 1);
                else if (i == 2) Console.SetCursorPosition(61, Console.WindowHeight - 1);
                else if (i == 3) Console.SetCursorPosition(91, Console.WindowHeight - 1);
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
            Console.SetCursorPosition(Console.WindowWidth-10, Console.WindowHeight - 1);
            Console.Write("Level: " + Library.getLevel());
        }
    }
}
