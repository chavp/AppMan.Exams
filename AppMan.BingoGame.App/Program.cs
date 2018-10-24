using System;

namespace AppMan.BingoGame.App
{
    using AppMan.Lib;
    using System.Collections.Generic;
    using static System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var bingoGame = new BingoGame();
            Display(bingoGame);
            WriteLine("-------------------------------");
            while (!bingoGame.IsBingo)
            {
                Write("Please input num [Enter]: ");
                string input_st = ReadLine();
                int input;
                if (int.TryParse(input_st, out input))
                {
                    bingoGame.Check(input);
                }
                Clear();
                Display(bingoGame);
                WriteLine("-------------------------------");
            }
            WriteLine("Bingo!");
            WriteLine("Exit [Enter]");
            ReadLine();
        }

        static void Display(BingoGame bingoGame)
        {
            for (int i = 0; i < bingoGame.Rows; i++)
            {
                for (int j = 0; j < bingoGame.Columns; j++)
                {
                    (int no, int check) = bingoGame.BingoBoard[(i, j)];
                    if (check == 1)
                    {
                        Write($" [{no}] ");
                    }
                    else
                    {
                        Write($" {no} ");
                    }
                }

                WriteLine();
            }
        }
    }
}
