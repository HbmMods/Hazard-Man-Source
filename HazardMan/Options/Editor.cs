using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Editor
    {
        public static void haxMode()
        {
            Console.Clear();
            Console.WriteLine("HaX Mode:");
            Console.WriteLine(" - Change Level [L]:");
            Console.WriteLine(" - Score changer [S]:");
            Console.WriteLine(" - Max Health Editor [H]:");
            Console.WriteLine("");
            ConsoleKey eingabe;
            try
            {
                eingabe = Console.ReadKey(true).Key;
            }
            catch
            {
                Console.WriteLine("This is not an available HaX!");
                return;
            }

            switch (eingabe)
            {
                case ConsoleKey.L:
                    changeLevel();
                    break;

                case ConsoleKey.S:
                    changeScore();
                    break;

                case ConsoleKey.H:
                    changeMaxHealth();
                    break;
            }
        }

        private static void changeLevel()
        {
            Console.Write("Change the current level (recommended not higher than 15): ");
            try
            {
                Library.setLevel(Convert.ToInt32(Console.ReadLine()));
                Console.Clear();
                Console.WriteLine("Changed level");
                return;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("This is not an available number!");
                return;
            }
        }

        private static void changeScore()
        {
            Console.Write("Enter player's name: ");
            string uuid = Console.ReadLine().ToUpper();
            foreach (OptionPlayer player in Library.players)
            {
                if (player.getUUID().Contains(uuid))
                {
                    Console.Write("Enter the score (recommended not higher than 10): ");
                    try
                    {
                        Library.score[player] = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine("Changed score");
                        return;
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("This is not an available number!");
                        return;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("There is no player with this name!");
            return;
        }

        private static void changeMaxHealth()
        {
            Console.Write("Change the max health (recommended not higher than 5): ");
            try
            {
                Library.setMaxHealth(Convert.ToInt32(Console.ReadLine()));
                Console.Clear();
                Console.WriteLine("Changed max health");
                return;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("This is not an available number!");
                return;
            }
        }
    }
}
