

using System;
using System.Collections.Generic;
using System.Threading;

class FallingRocks
{
    struct Dwarf
    {
        public int x;
        public int y;
        public string c;
        public ConsoleColor color;
    }

    struct Rock
    {
        public int x;
        public int y;
        public string c;
        public ConsoleColor color;
    }

    static void StickOnPosition(int x, int y, string c, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }

    static void FallingRocksRain(int x, int y, string c, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }

    static void stickLivesView(int x, int y, string txtLives, int live, string txtScore, int score)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(txtLives);
        Console.Write(live);
        Console.SetCursorPosition(x, y + 1);
        Console.Write(txtScore);
        Console.Write(score);
    }

    static void Main()
    {
        Console.BufferHeight = Console.WindowHeight = 20; // Vertical Y
        Console.BufferWidth = Console.WindowWidth = 38; // Horizontal X
        Dwarf stick = new Dwarf();
        int playfieldWidth = 18;
        int stickLives = 5;
        int result = 0;
        int time = 0;

        stick.x = 9;
        stick.y = Console.WindowHeight - 1;
        stick.color = ConsoleColor.Yellow;
        stick.c = "(0)";

        Random rand = new Random();

        Rock newRocks = new Rock();
        string[] element = { "^", "@", "&", "%", "*", "#", "!", ".", ";", "=" };
        List<Rock> rocks = new List<Rock>();

        StickOnPosition(stick.x, stick.y, stick.c, ConsoleColor.Yellow);

        while (true)
        {
            {
                newRocks.y = 0;
                newRocks.x = rand.Next(1, playfieldWidth + 3);
                newRocks.color = ConsoleColor.Green;//FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                newRocks.c = element[rand.Next(0, 10)];
                if (rand.Next(1, 115) < 30)
                {
                    if (rand.Next(1, 115) < 13)
                    {
                        newRocks.color = ConsoleColor.Red;
                        newRocks.c = "+";
                    }
                    else
                    {
                        newRocks.color = ConsoleColor.Yellow;
                        newRocks.c = "$";
                    }
                }
                rocks.Add(newRocks);
            }
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (stick.x - 1 >= 0)
                    {
                        stick.x -= 1;
                    }
                }
                if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (stick.x + 1 <= playfieldWidth)
                    {
                        stick.x += 1;
                    }
                }
            }
            StickOnPosition(stick.x, stick.y, stick.c, ConsoleColor.Yellow);
            if ((500 - time) > 150)
            {
                Thread.Sleep(500 - time);
            }
            else
            {
                Thread.Sleep(150);
            }
            Console.Clear();

            List<Rock> newList = new List<Rock>();
            for (int i = 0; i < rocks.Count; i++)
            {
                Rock oldRocks = rocks[i];
                Rock newRockses = new Rock();
                newRockses.x = oldRocks.x;
                newRockses.y = oldRocks.y + 1;
                newRockses.c = oldRocks.c;
                newRockses.color = oldRocks.color;


                if ((stick.x == newRockses.x && stick.y == newRockses.y) || (stick.x + 1 == newRockses.x && stick.y == newRockses.y) || (stick.x + 2 == newRockses.x && stick.y == newRockses.y))
                {
                    if (newRockses.c == "$")
                    {
                        result++;
                        if (result % 5 == 0)
                        {
                            time += 25;
                        }
                        continue;
                    }
                    if (newRockses.c == "+")
                    {
                        stickLives++;
                        continue;
                    }
                    stickLives--;

                    for (int j = 0; j < 240; j++)
                    {
                        StickOnPosition(stick.x + 1, stick.y, "X", ConsoleColor.Red);
                    }


                    if (stickLives == 0)
                    {
                        stickLivesView(25, 10, "Lives : ", stickLives, "Score : ", result);
                        Console.SetCursorPosition(7, 13);
                        Console.WriteLine("Press any key to exit !!");
                        Console.ReadLine();
                        return;
                    }
                    break;
                }
                if (newRockses.y < Console.WindowHeight)
                {
                    newList.Add(newRockses);
                }
            }
            rocks = newList;
            stickLivesView(25, 10, "Lives : ", stickLives, "Score : ", result);
            foreach (Rock roc in rocks)
            {
                FallingRocksRain(roc.x, roc.y, roc.c, roc.color);
            }
        }
    }
}

