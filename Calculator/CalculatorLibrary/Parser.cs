using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    public class Parser
    {
        Tokenizer _tokenizer;

        // Constructor - just store the tokenizer
        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        // Parse an entire expression and check EOF was reached
        public Node ParseExpression()
        {
            //  Parse add/substract operators first to achieve correct operator priority
            var expr = ParseAddSubtract();

            // Check everything was consumed
            if (_tokenizer.Token != Token.EOF)
                throw new SyntaxException("Unexpected characters at end of expression");

            return expr;
        }

        // Parse a sequence of add/subtract operators
        Node ParseAddSubtract()
        {
            // Parse the left hand side as multiply/divide to achieve correct operator priority
            var lhs = ParseMultiplyDivide();

            while (true)
            {
                // Work out the operator
                Func<double, double, double>? op = null;
                //  If operator is add store addition lambda expression
                if (_tokenizer.Token == Token.Add)
                {
                    op = (a, b) => a + b;
                }
                //  If operator is subtract store subtraction lambda expression
                else if (_tokenizer.Token == Token.Subtract)
                {
                    op = (a, b) => a - b;
                }

                //  If no more operators are found it means we reached end of expression,
                //  return left hand side which contains the whole expression tree
                if (op == null)
                    return lhs; 

                //  Skip the operator token, we already stored it
                _tokenizer.NextToken();

                //  Parse the right hand side of the expression
                var rhs = ParseMultiplyDivide();

                //  Create a binary node and use it as the left-hand side from now on
                lhs = new OpNode(lhs, rhs, op);
            }
        }

        // Parse a sequence of multiply/divide operators
        Node ParseMultiplyDivide()
        {
            //Parse the left hand side as unary, to achieve correct operator preference
            var lhs = ParseUnary();

            while (true)
            {
                // Work out the operator
                Func<double, double, double>? op = null;
                // If operator is multiply store multiplication lambda expression
                if (_tokenizer.Token == Token.Multiply)
                {
                    op = (a, b) => a * b;
                }
                // If operator is divide store division lambda expression
                else if (_tokenizer.Token == Token.Divide)
                {
                    op = (a, b) => a / b;
                }

                //  If no more operators are found it means we reached end of expression,
                //  return left hand side which contains the whole expression tree
                if (op == null)
                    return lhs;

                // Skip the operator token, we already stored it
                _tokenizer.NextToken();

                // Parse the right hand side of the expression
                var rhs = ParseUnary();

                // Create a binary node and use it as the left-hand side from now on
                lhs = new OpNode(lhs, rhs, op);
            }
        }


        // Parse a unary operator (eg: negative/positive)
        Node ParseUnary()
        {
            while (true)
            {
                // Positive operator is a no-op so just skip it
                if (_tokenizer.Token == Token.Add)
                {
                    // Skip
                    _tokenizer.NextToken();
                    continue;
                }

                // Negative operator
                if (_tokenizer.Token == Token.Subtract)
                {
                    // Skip
                    _tokenizer.NextToken();

                    // Parse RHS 
                    // Note this recurses to self to support negative of a negative
                    var rhs = ParseUnary();

                    // Create unary node with negative of x lambda expression as operator
                    return new UnaryNode(rhs, (a) => -a);
                }

                // No positive/negative operator so parse a leaf node
                return ParseLeaf();
            }
        }

        // Parse a leaf node
        Node ParseLeaf()
        {
            if (_tokenizer.Token == Token.Number)
            {
                var node = new NumberNode(_tokenizer.Number);
                _tokenizer.NextToken();
                return node;
            }

            // Don't Understand
            throw new SyntaxException($"Unexpected token: {_tokenizer.Token}");
        }


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
    }

}
