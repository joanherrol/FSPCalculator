using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    internal class OpNode : Node
    {
        //  Binary node that represents one of the four available operations
        public OpNode(Node lhs, Node rhs, Func<double, double, double> op)
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

            //Check for rhs 0 division
            if (Double.IsInfinity(result)) 
                throw new EvalException("Division by 0");

            return result;
        }
    }
}
