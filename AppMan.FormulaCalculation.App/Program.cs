namespace AppMan.FormulaCalculation.App
{
    using AppMan.Lib.Expression;
    using System;
    using static System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;
            WriteLine("(Exit if input is Q) [ENTER].");
            while (true)
            {
                Write("Please input math expression : ");
                input = ReadLine();
                if (input == "") break;

                try
                {
                    var result = Parser.Parse(input).Eval();
                    WriteLine($"  {input} = {result}");
                }
                catch (ArgumentException ex) {
                    WriteLine($"  {ex.Message}");
                }
            }
        }
    }
}
