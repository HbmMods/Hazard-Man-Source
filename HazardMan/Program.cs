using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace HazardMan
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console Height: 30
            //Console Width: 120
            Console.OutputEncoding = Encoding.UTF8;
            int selection = 0;
            bool loop = true;
            bool masterloop = true;
            const int yOffset = 2;
            ConsoleKey input;
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            Library.players.Add(new Option_Player("Gregoll", ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleColor.Cyan));

            while (masterloop)
            {
                loop = true;
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;

                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    for (int j = 0; j < Console.WindowWidth; j++)
                    {
                        Console.Write(" ");
                    }
                }

                DrawText(Library.s1, Console.WindowWidth / 2 - Library.s1.Length / 2, yOffset + 0);
                DrawText(Library.s2, Console.WindowWidth / 2 - Library.s2.Length / 2, yOffset + 1);
                DrawText(Library.s3, Console.WindowWidth / 2 - Library.s3.Length / 2, yOffset + 2);
                DrawText(Library.s4, Console.WindowWidth / 2 - Library.s4.Length / 2, yOffset + 3);
                DrawText(Library.s5, Console.WindowWidth / 2 - Library.s5.Length / 2, yOffset + 4);
                DrawText(Library.s6, Console.WindowWidth / 2 - Library.s6.Length / 2, yOffset + 5);
                DrawText(Library.s7, Console.WindowWidth / 2 - Library.s7.Length / 2, yOffset + 6);
                DrawText(Library.s8, Console.WindowWidth / 2 - Library.s8.Length / 2, yOffset + 7);
                DrawText(Library.s9, Console.WindowWidth / 2 - Library.s9.Length / 2, yOffset + 8);
                DrawText(Library.s10, Console.WindowWidth / 2 - Library.s10.Length / 2, yOffset + 9);
                DrawText(Library.s11, Console.WindowWidth / 2 - Library.s11.Length / 2, yOffset + 10);
                DrawText(Library.s12, Console.WindowWidth / 2 - Library.s12.Length / 2, yOffset + 11);
                DrawText(Library.s13, Console.WindowWidth / 2 - Library.s13.Length / 2, yOffset + 12);
                DrawText(Library.s14, Console.WindowWidth / 2 - Library.s14.Length / 2, yOffset + 13);
                DrawText(Library.s15, Console.WindowWidth / 2 - Library.s15.Length / 2, yOffset + 14);
                Console.ForegroundColor = ConsoleColor.Gray;

                while (loop)
                {
                    if (selection == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    DrawText("Play Game", Console.WindowWidth / 2 - "Play Game".Length / 2, yOffset + 18);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (selection == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    DrawText("Options", Console.WindowWidth / 2 - "Options".Length / 2, yOffset + 20);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    if (selection == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    DrawText("Quit", Console.WindowWidth / 2 - "Quit".Length / 2, yOffset + 22);
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
                                    if (Library.players.Count == 0)
                                    {
                                        Console.SetCursorPosition(1, 28);
                                        Console.WriteLine("Please create a player at first");
                                    }
                                    else
                                    {
                                        World.StartWorld();
                                        while (World.tickWorld)
                                        { }
                                        loop = false;
                                    }   
                                    break;
                                }
                            case 1:
                                {
                                    Option.createOptionMenu();
                                    loop = false;
                                    break;
                                }
                            case 2:
                                {
                                    masterloop = false;
                                    loop = false;
                                    break;
                                }
                        }
                    }
                }
            }
        }

        public static void DrawText(string s, int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(s);
        }
    }
}
