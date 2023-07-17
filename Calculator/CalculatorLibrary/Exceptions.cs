using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    // Exception for syntax errors (for readability) 
    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
        }
    }

    // Exception for evaluation errors (for readability) 
    public class EvalException : Exception
    {

        public EvalException(string message)
            : base(message)
        {
        }
    }
}
