using System;
using System.Linq;
using System.Collections.Generic;

namespace AppMan.Lib
{
    public class BingoGame
    {
        public static int BoardSize { get;  } = 5;

        Dictionary<(int, int), (int, int)> BingoBoard { get; } 

        public BingoGame()
        {
            BingoBoard = newBoard();
        }

        public (Dictionary<(int, int), (int, int)>, bool) Step(params int[] input)
        {
            check(input);
            return (BingoBoard, isBingo);
        }

        private Dictionary<(int, int), (int, int)> newBoard()
        {
            var bingoBoard = new Dictionary<(int, int), (int, int)>();
            int no = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    bingoBoard.Add((i, j), (++no, 0));
                }
            }
            return bingoBoard;
        }

        private void check(params int[] input)
        {
            foreach (var num in input)
            {
                if (num <= BoardSize * BoardSize)
                {
                    var row_col = BingoBoard.Where(x => x.Value.Item1 == num).Single().Key;
                    BingoBoard[row_col] = (num, 1);
                }
            }
        }

        private bool isBingo
        {
            get
            {
                bool isBingo = false;
                var sums = new List<int>();
                var sum1 = 0;
                var sum2 = 0;

                for (int i = 0; i < BoardSize; i++)
                {
                    var sumRow = BingoBoard
                        .Where(x => x.Key.Item1 == i)
                        .Sum(y => y.Value.Item2);
                    sums.Add(sumRow);

                    var sumCol = BingoBoard
                        .Where(x => x.Key.Item2 == i)
                        .Sum(y => y.Value.Item2);
                    sums.Add(sumCol);

                    sum1 += BingoBoard
                        .Where(x => x.Key.Item1 == i && x.Key.Item2 == i)
                        .Single().Value.Item2;

                    sum2 += BingoBoard
                        .Where(x => x.Key.Item1 == i && x.Key.Item2 == BoardSize - 1 - i)
                        .Single().Value.Item2;
                }

                sums.Add(sum1);
                sums.Add(sum2);

                foreach (var sum in sums)
                {
                    if (sum == BoardSize)
                    {
                        isBingo = true;
                        break;
                    }
                }
                return isBingo;
            }
        }

    }
}
