using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xunit;

namespace AppMan.Exams
{
    public class FormulaCalculationExam
    {
        [Theory]
        [InlineData("(22*2) + 50", 94)]
        //[InlineData("((2*3+12) / 2)", 15)]
        //[InlineData("(10-5+3/2*2)", 8)]
        public void Examples(string input, int expected)
        {
            int result = 0;

            var t = new Tokenizer(new StringReader(input));

            Assert.Equal(expected, result);
        }
    }

    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Number,
        Multiply,
        Divide,
        Open,
        Close,
    }

    /// <summary>
    /// https://medium.com/@CantabileApp/writing-a-simple-math-expression-engine-in-c-d414de18d4ce
    /// </summary>
    public class Tokenizer
    {
        public Tokenizer(TextReader reader)
        {
            _reader = reader;
            NextChar();
            NextToken();
        }

        TextReader _reader;
        char _currentChar;
        Token _currentToken;
        double _number;

        public Token Token
        {
            get { return _currentToken; }
        }

        public double Number
        {
            get { return _number; }
        }

        // Read the next character from the input strem
        // and store it in _currentChar, or load '\0' if EOF
        void NextChar()
        {
            int ch = _reader.Read();
            _currentChar = ch < 0 ? '\0' : (char)ch;
        }

        // Read the next token from the input stream
        public void NextToken()
        {
            // Skip whitespace
            while (char.IsWhiteSpace(_currentChar))
            {
                NextChar();
            }

            // Special characters
            switch (_currentChar)
            {
                case '\0':
                    _currentToken = Token.EOF;
                    return;

                case '+':
                    NextChar();
                    _currentToken = Token.Add;
                    return;

                case '-':
                    NextChar();
                    _currentToken = Token.Subtract;
                    return;

                case '*':
                    NextChar();
                    _currentToken = Token.Multiply;
                    return;

                case '/':
                    NextChar();
                    _currentToken = Token.Divide;
                    return;
            }

            // Number?
            if (char.IsDigit(_currentChar) || _currentChar == '.')
            {
                // Capture digits/decimal point
                var sb = new StringBuilder();
                bool haveDecimalPoint = false;
                while (char.IsDigit(_currentChar) || (!haveDecimalPoint && _currentChar == '.'))
                {
                    sb.Append(_currentChar);
                    haveDecimalPoint = _currentChar == '.';
                    NextChar();
                }

                // Parse it
                _number = double.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                _currentToken = Token.Number;
                return;
            }

            if(_currentChar == '(')
            {
                var sb = new StringBuilder();
                while (char.IsDigit(_currentChar))
                {
                    sb.Append(_currentChar);
                    NextChar();
                }

                _number = double.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                _currentToken = Token.Number;
                return;
            }

            throw new InvalidDataException($"Unexpected character: {_currentChar}");
        }
    }
}
