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
            (Dictionary<(int, int), (int, int)> board, bool isBingo) = bingoGame.Step();
            Display(board);
            WriteLine("-------------------------------");
            while (!isBingo)
            {
                Write("Please input num [Enter]: ");
                string input_st = ReadLine();
                int input;
                if (int.TryParse(input_st, out input))
                {
                    (board, isBingo) = bingoGame.Step(input);
                    Clear();
                    Display(board);
                }
                WriteLine("-------------------------------");
            }
            WriteLine("Bingo!");
            WriteLine("Exit [Enter]");
            ReadLine();
        }

        static void Display(Dictionary<(int, int), (int, int)> board)
        {
            for (int i = 0; i < BingoGame.Rows; i++)
            {
                for (int j = 0; j < BingoGame.Columns; j++)
                {
                    (int no, int check) = board[(i, j)];
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
