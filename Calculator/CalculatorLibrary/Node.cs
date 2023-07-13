using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibrary
{
    // Node - abstract class representing one node in the expression 
    public abstract class Node
    {
        public abstract double Eval();
    }
}
