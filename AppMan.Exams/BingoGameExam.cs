using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace AppMan.Exams
{
    using AppMan.Lib;

    public class BingoGameExam
    {
        [Theory]
        [InlineData(new int[] { 3, 4, 8, 13, 18, 19, 23 }, true )]
        [InlineData(new int[] { 1, 13, 19, 25, 23, 2 }, false)]
        [InlineData(new int[] { 2, 1, 12, 15, 6, 18, 16, 4, 3, 21, 11 }, true)]
        public void Examples(int[] input, bool expected)
        {
            var bingoBoard = new BingoGame();
            (Dictionary<(int, int), (int, int)> board, bool isBingo) = bingoBoard.Step(input);
            Assert.Equal(expected, isBingo);
        }
    }
}
