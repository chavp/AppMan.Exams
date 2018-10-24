using System;
using System.Linq;
using System.Collections.Generic;

namespace AppMan.Lib
{
    public class BingoGame
    {
        public int Columns { get;  } = 5;
        public int Rows { get; } = 5;

        public Dictionary<(int, int), (int, int)> BingoBoard { get; } = new Dictionary<(int, int), (int, int)>();

        public BingoGame()
        {
            BingoBoard = NewBoard();
        }

        Dictionary<(int, int), (int, int)> NewBoard()
        {
            var bingoBoard = new Dictionary<(int, int), (int, int)>();
            int no = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    bingoBoard.Add((i, j), (++no, 0));
                }
            }
            return bingoBoard;
        }

        public void Check(params int[] input)
        {
            foreach (var num in input)
            {
                var row_col = BingoBoard.Where(x => x.Value.Item1 == num).Single().Key;
                BingoBoard[row_col] = (num, 1);
            }
        }

        public bool IsBingo
        {
            get
            {
                bool isBingo = false;
                var sums = new List<int>();

                for (int i = 0; i < Rows; i++)
                {
                    var sum = BingoBoard.Where(x => x.Key.Item1 == i).Sum(y => y.Value.Item2);
                    sums.Add(sum);
                }

                for (int i = 0; i < Columns; i++)
                {
                    var sum = BingoBoard.Where(x => x.Key.Item2 == i).Sum(y => y.Value.Item2);
                    sums.Add(sum);
                }

                var sum1 = 0;
                var sum2 = 0;
                for (int i = 0; i < Rows; i++)
                {
                    sum1 += BingoBoard.Where(x => x.Key.Item1 == i && x.Key.Item2 == i).Single().Value.Item2;
                    sum2 += BingoBoard.Where(x => x.Key.Item1 == i && x.Key.Item2 == Columns - 1 - i).Single().Value.Item2;
                }
                sums.Add(sum1);
                sums.Add(sum2);

                foreach (var sum in sums)
                {
                    if (sum == Rows)
                    {
                        isBingo = true;
                    }
                    else if (sum == Rows * Columns)
                    {
                        isBingo = false;
                    }
                }
                return isBingo;
            }
        }


    }
}
