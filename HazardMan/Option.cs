using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Option
    {
        public static void createOptionMenu()
        {
            Console.Clear();
            ConsoleKey input;
            bool running = true;

            while (running)
            {
                if(!(Library.players.Count == 0)) Console.WriteLine("All players: ");
                foreach (Option_Player player in Library.players)
                {
                    int i = 1;
                    Console.WriteLine(i + ": Name: " + player.getName() + " | Jump Key: " + player.getUpKey() + " | Left Key: " + player.getLeftKey() + " | Right Key: " + player.getRightKey() + " | Color: " + player.getColor().ToString());
                    i++;
                }
                Console.WriteLine("");
                Console.WriteLine("Add player: [A]");
                Console.WriteLine("Remove player: [R]");
                Console.WriteLine("Toggle sounds: [S]");
                Console.WriteLine("Exit to main menu: [X]");

                input = Console.ReadKey(true).Key;

                switch(input)
                {
                    case ConsoleKey.A:
                        createPlayer();

                        break;

                    case ConsoleKey.R:
                        Console.Write("Enter player name: ");
                        string uuid = Console.ReadLine().ToUpper();

                        Option_Player playerfordelete = null;

                        foreach(Option_Player player in Library.players) {
                            if(uuid.Contains(player.getUUID()))
                            {
                                playerfordelete = player;
                                break;
                            }
                        }

                        Console.Clear();
                        if (playerfordelete != null)
                        {
                            Library.players.Remove(playerfordelete);
                            Console.WriteLine("Succsessfully deleted player!");
                        }
                        else
                        {
                            Console.WriteLine("There is no player with this name");
                        }

                        break;

                    case ConsoleKey.S:
                        Console.Clear();
                        if (Library.isSoundActivated)
                        {
                            Library.isSoundActivated = false;
                            Console.WriteLine("Sounds deactivated");
                        }
                        else
                        {
                            Library.isSoundActivated = true;
                            Console.WriteLine("Sounds activated");
                        }
                        break;

                    case ConsoleKey.X:
                        running = false;
                        break;

                    case ConsoleKey.Escape:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }
        }

        private static void createPlayer()
        {
            if (Library.players.Count > 4)
            {
                Console.Clear();
                Console.WriteLine("There are already 4 players!");
                return;
            }

            Console.Write("Enter player name: ");
            string name = Console.ReadLine();
            foreach (Option_Player player in Library.players)
            {
                if (name.ToUpper().Contains(player.getUUID()))
                {
                    Console.Clear();
                    Console.WriteLine("This player already exists!");
                    return;
                }
            }

            Console.Write("Bind jump key: ");
            ConsoleKey jump = Console.ReadKey(true).Key;
            Console.WriteLine(jump.ToString());
            Console.Write("Bind left key: ");
            ConsoleKey left = Console.ReadKey(true).Key;
            Console.WriteLine(left.ToString());
            Console.Write("Bind right key: ");
            ConsoleKey right = Console.ReadKey(true).Key;
            Console.WriteLine(right.ToString());
            Console.WriteLine("Choose a color:");
            List<ConsoleColor> colors = new List<ConsoleColor>();
            colors.Add(ConsoleColor.Red);
            colors.Add(ConsoleColor.Blue);
            colors.Add(ConsoleColor.Green);
            colors.Add(ConsoleColor.Yellow);
            foreach (ConsoleColor forcolor in colors)
            {
                bool iscolor = false;
                foreach (Option_Player player in Library.players)
                {
                    if (player.getColor() == forcolor)
                    {
                        iscolor = true;
                        break;
                    }
                }

                if (!iscolor) Console.WriteLine(" - " + forcolor.ToString());
            }
            ConsoleColor color = ConsoleColor.DarkGray;
            switch (Console.ReadLine().ToUpper())
            {
                case "RED":
                    color = ConsoleColor.Red;
                    break;

                case "BLUE":
                    color = ConsoleColor.Blue;
                    break;

                case "GREEN":
                    color = ConsoleColor.Green;
                    break;

                case "YELLOW":
                    color = ConsoleColor.Yellow;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("This isn't an available color!");
                    return;
            }

            if (color != ConsoleColor.DarkGray)
            {
                foreach (Option_Player player in Library.players)
                {
                    if (player.getColor() == color)
                    {
                        Console.Clear();
                        Console.WriteLine("This color is already choosen!");
                        return;
                    }
                }

                Library.players.Add(new Option_Player(name, jump, left, right, color));
                Console.Clear();
                Console.WriteLine("Player succsessfully created!");
            }
        }
    }
}
