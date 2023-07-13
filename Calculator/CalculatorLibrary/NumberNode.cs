using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    internal class NumberNode : Node
    {
        //Leaf node representing a number (double)
        public NumberNode(double number)
        {
            _number = number;
        }

        double _number;          

        public override double Eval()
        {
            return _number;
        }
    }
}
