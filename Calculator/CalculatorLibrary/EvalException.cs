using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    // Exception for eval errors (for readability) 
    public class EvalException : Exception
    {
        public EvalException(string message)
            : base(message)
        {
        }
    }
}
