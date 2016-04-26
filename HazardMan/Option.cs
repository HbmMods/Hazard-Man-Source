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
                    Console.WriteLine("Name: " + player.getName() + " | Jump Key: " + player.getUpKey() + " | Left Key: " + player.getLeftKey() + " | Right Key: " + player.getRightKey() + " | Color: " + player.getColor().ToString());
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
                        Console.Write("Enter player name: ");
                        string name = Console.ReadLine();
                        bool wanttobreak = false;
                        foreach(Option_Player player in Library.players)
                        {
                            if(name.ToUpper().Contains(player.getUUID())) {
                                Console.Clear();
                                Console.WriteLine("This player already exists!");
                                wanttobreak = true;
                                break;
                            }
                        }
                        if (wanttobreak) break;
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
                            foreach(Option_Player player in Library.players)
                            {
                                if(player.getColor() == forcolor)
                                {
                                    iscolor = true;
                                    break;
                                }
                            }

                            if (!iscolor) Console.WriteLine(" - " + forcolor.ToString());
                        }
                        ConsoleColor color = ConsoleColor.DarkGray;
                        switch(Console.ReadLine().ToUpper())
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
                                wanttobreak = true;
                                break;
                        }

                        if (color != ConsoleColor.DarkGray)
                        {
                            bool inwantbreak = false;
                            foreach(Option_Player player in Library.players)
                            {
                                if(player.getColor() == color)
                                {
                                    Console.Clear();
                                    Console.WriteLine("This color is already choosen!");
                                    wanttobreak = true;
                                    inwantbreak = true;
                                    break;
                                }
                            }

                            if (inwantbreak) break;

                            Library.players.Add(new Option_Player(name, jump, left, right, color));
                            Console.Clear();
                            Console.WriteLine("Player succsessfully created!");
                        }

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



            /*const int yOffset = 2;
            int selection = 0;
            bool all = true;
            bool selectionswitch = true;

            while (all)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                Program.DrawText(Library.option_name_00, Console.WindowWidth / 2 - Library.option_name_00.Length / 2, yOffset + 0);
                Program.DrawText(Library.option_name_01, Console.WindowWidth / 2 - Library.option_name_00.Length / 2, yOffset + 1);
                Program.DrawText(Library.option_name_02, Console.WindowWidth / 2 - Library.option_name_00.Length / 2, yOffset + 2);
                Program.DrawText(Library.option_name_03, Console.WindowWidth / 2 - Library.option_name_00.Length / 2, yOffset + 3);
                Program.DrawText(Library.option_name_04, Console.WindowWidth / 2 - Library.option_name_00.Length / 2, yOffset + 4);

                while (selectionswitch)
                {
                    if (selection == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Program.DrawText("Player 1:", Console.WindowWidth / 2 - "Player 1:".Length / 2, yOffset + 10);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (selection == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Program.DrawText("Player 2:", Console.WindowWidth / 2 - "Player 2:".Length / 2, yOffset + 12);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (selection == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Program.DrawText("Quit", Console.WindowWidth / 2 - "Quit".Length / 2, yOffset + 14);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    input = Console.ReadKey(true).Key;

                    if (input == ConsoleKey.UpArrow || input == ConsoleKey.W)
                    {
                        selection -= 1;
                    }

                    if (input == ConsoleKey.DownArrow || input == ConsoleKey.S)
                    {
                        selection += 1;
                    }

                    if (selection < 0)
                    {
                        selection = 2;
                    }

                    if (selection > 2)
                    {
                        selection = 0;
                    }

                    if (input == ConsoleKey.Enter || input == ConsoleKey.Spacebar)
                    {
                        switch (selection)
                        {
                            case 0:
                                {
                                    World.StartWorld();
                                    while (World.tickWorld)
                                    { }
                                    goto start;
                                }
                            case 1:
                                {
                                    break;
                                }
                            case 2:
                                {
                                    loop = false;
                                    break;
                                }
                        }
                    }
                }
            }*/
        }
    }
}
