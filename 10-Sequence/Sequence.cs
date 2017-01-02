using System;

    class Sequence
    {
        static void Main()
        {
            for (int i = 2; i < 103; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write(i + " ");
                }
                else
                {
                    Console.Write(-i + "  ");
                }
            }
          
            Console.WriteLine();
        }
    }

