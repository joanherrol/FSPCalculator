using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    internal class UnaryNode : Node
    {
        // Node that performs unary operations (eg: positive|negative)
        public UnaryNode(Node rhs, Func<double, double> op)
        {
            _rhs = rhs;
            _op = op;
        }

        Node _rhs;                              // Right hand side of the operation
        Func<double, double> _op;               // The callback operator

        public override double Eval()
        {
            // Evaluate RHS
            var rhsVal = _rhs.Eval();

            // Evaluate and return
            var result = _op(rhsVal);
            return result;
        }
    }
}
