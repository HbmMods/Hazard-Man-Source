using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Option
    {
        public static List<Option_Player> players = new List<Option_Player>();

        public static void createOptionMenu()
        {
            Console.Clear();
            char input;
            bool running = true;

            while (running)
            {
                if(!(players.Count == 0)) Console.WriteLine("[HazardMan]All players: ");
                foreach (Option_Player player in players)
                {
                    Console.WriteLine("[HazardMan]Name: " + player.getName() + " | Jump key: " + player.getUpKey() + " | Left key: " + player.getLeftKey() + " | Right key: " + player.getRightKey());
                }
                Console.WriteLine("");
                Console.WriteLine("[HazardMan]Add player: [A]");
                Console.WriteLine("[HazardMan]Remove player: [R]");
                Console.WriteLine("[HazardMan]Activate sounds: [S]");
                Console.WriteLine("[HazardMan]Exit to start menu: [X]");
                Console.Write("[HazardMan]Your input: ");
                try
                {
                    input = Convert.ToChar(Console.ReadLine().ToUpper());
                }
                catch (Exception)
                {
                    input = 'Z';
                }

                switch(input)
                {
                    case 'A':
                        Console.Write("[HazardMan]Enter name: ");
                        string name = Console.ReadLine();
                        bool wanttobreak = false;
                        foreach(Option_Player player in players)
                        {
                            if(name.ToUpper().Contains(player.getUUID())) {
                                Console.Clear();
                                Console.WriteLine("[HazardMan]This player already exists!");
                                wanttobreak = true;
                                break;
                            }
                        }
                        if (wanttobreak) break;
                        Console.WriteLine("[HazardMan]Enter jump key: ");
                        ConsoleKey jump = Console.ReadKey(true).Key;
                        Console.WriteLine("[HazardMan]Enter left key: ");
                        ConsoleKey left = Console.ReadKey(true).Key;
                        Console.WriteLine("[HazardMan]Enter rigth key: ");
                        ConsoleKey rigth = Console.ReadKey(true).Key;

                        players.Add(new Option_Player(name, jump, left, rigth));
                        Console.Clear();
                        Console.WriteLine("[HazardMan]Created player successful!");
                        break;

                    case 'R':
                        Console.Write("[HazardMan]Enter name: ");
                        string uuid = Console.ReadLine().ToUpper();

                        Option_Player playerfordelete = null;

                        foreach(Option_Player player in players) {
                            if(uuid.Contains(player.getUUID()))
                            {
                                playerfordelete = player;
                                break;
                            }
                        }

                        Console.Clear();
                        if (playerfordelete != null)
                        {
                            players.Remove(playerfordelete);
                            Console.WriteLine("[HazardMan]Deleted player successful");
                        }
                        else
                        {
                            Console.WriteLine("[HazardMan]There is no player with this name");
                        }

                        break;

                    case 'S':
                        if (Library.isSoundActivated)
                        {
                            Library.isSoundActivated = false;
                            Console.WriteLine("[HazardMan]Deactivated sounds");
                        }
                        else
                        {
                            Library.isSoundActivated = true;
                            Console.WriteLine("[HazardMan]Activated sounds");
                        }
                        break;

                    case 'X':
                        running = false;
                        break;

                    default:
                        Console.WriteLine("[HazardMan]False input");
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
