using System;
using System.Collections.Generic;
using System.Text;

namespace AppMan.Lib.Expression
{
    public abstract class Node
    {
        public abstract double Eval();
    }

    public class NodeNumber : Node
    {
        public NodeNumber(double number)
        {
            _number = number;
        }

        double _number;             // The number

        public override double Eval()
        {
            // Just return it.  Too easy.
            return _number;
        }
    }
}
