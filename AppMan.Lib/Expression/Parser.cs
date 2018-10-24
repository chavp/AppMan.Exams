using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppMan.Lib.Expression
{
    public class Parser
    {
        // Constructor - just store the tokenizer
        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        Tokenizer _tokenizer;

        // Parse an entire expression and check EOF was reached
        public Node ParseExpression()
        {
            // For the moment, all we understand is add and subtract
            var expr = ParseAddSubtract();

            // Check everything was consumed
            if (_tokenizer.Token != Token.EOF)
                throw new ArgumentException("Unexpected characters at end of expression");

            return expr;
        }

        // Parse an sequence of add/subtract operators
        Node ParseAddSubtract()
        {
            // Parse the left hand side
            var lhs = ParseLeaf();

            while (true)
            {
                // Work out the operator
                Func<double, double, double> op = null;
                switch (_tokenizer.Token)
                {
                    case Token.Add:
                        op = (a, b) => a + b;
                        break;
                    case Token.Subtract:
                        op = (a, b) => a - b;
                        break;
                    case Token.Multiply:
                        op = (a, b) => a * b;
                        break;
                    case Token.Divide:
                        op = (a, b) => a / b;
                        break;
                    default:
                        break;
                }

                // Binary operator found?
                if (op == null)
                    return lhs;             // no

                // Skip the operator
                _tokenizer.NextToken();

                // Parse the right hand side of the expression
                var rhs = ParseLeaf();

                // Create a binary node and use it as the left-hand side from now on
                lhs = new NodeBinary(lhs, rhs, op);
            }
        }

        // Parse a leaf node
        // (For the moment this is just a number)
        Node ParseLeaf()
        {
            // Is it a number?
            if (_tokenizer.Token == Token.Number)
            {
                var node = new NodeNumber(_tokenizer.Number);
                _tokenizer.NextToken();
                return node;
            }

            // Parenthesis?
            if (_tokenizer.Token == Token.OpenParents)
            {
                _tokenizer.NextToken();

                var node = ParseAddSubtract();
                if (_tokenizer.Token != Token.CloseParents)
                    throw new ArgumentException("Missing close parenthesis");
                _tokenizer.NextToken();

                return node;
            }

            // Don't Understand
            throw new ArgumentException($"Unexpect token: {_tokenizer.Token}");
        }


        #region Convenience Helpers

        // Static helper to parse a string
        public static Node Parse(string str)
        {
            return Parse(new Tokenizer(new StringReader(str)));
        }

        // Static helper to parse from a tokenizer
        public static Node Parse(Tokenizer tokenizer)
        {
            var parser = new Parser(tokenizer);
            return parser.ParseExpression();
        }

        #endregion
    }
}
