﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Multiply,
        Divide,
        Number
    }

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

        // Read the next character from the input stream
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

            else throw new SyntaxException("Syntax error. Remember to only use numbers and +, -, *, /");
        }
    }
}
