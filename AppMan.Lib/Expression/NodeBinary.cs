﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppMan.Lib.Expression
{
    class NodeBinary : Node
    {
        // Constructor accepts the two nodes to be operated on and function
        // that performs the actual operation
        public NodeBinary(Node lhs, Node rhs, Func<double, double, double> op)
        {
            _lhs = lhs;
            _rhs = rhs;
            _op = op;
        }

        Node _lhs;                              // Left hand side of the operation
        Node _rhs;                              // Right hand side of the operation
        Func<double, double, double> _op;       // The callback operator

        public override double Eval()
        {
            // Evaluate both sides
            var lhsVal = _lhs.Eval();
            var rhsVal = _rhs.Eval();

            // Evaluate and return
            var result = _op(lhsVal, rhsVal);
            return result;
        }
    }
}
