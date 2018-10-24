using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xunit;

namespace AppMan.Exams
{
    /// <summary>
    /// https://medium.com/@CantabileApp/writing-a-simple-math-expression-engine-in-c-d414de18d4ce
    /// </summary>
    public class FormulaCalculationExam
    {
        [Theory]
        [InlineData("(22*2) + 50", 94)]
        //[InlineData("((2*3+12) / 2)", 15)]
        //[InlineData("(10-5+3/2*2)", 8)]
        public void Examples(string input, int expected)
        {
            int result = 0;

 
        }
    }
}
