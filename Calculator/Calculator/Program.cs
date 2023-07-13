using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            // Display title as the C# console calculator app.
            Console.WriteLine("INSTRUCTIONS\r");
            Console.WriteLine("All operands have to be numbers. Operators accepted by the calculator are:\n");
            Console.WriteLine("Addition (+) Substraction (-) Negation (-) Multiplication (*) Division (/)\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string inputExpr = "";

                // Ask the user to type the first number.
                Console.Write("Type a mathematical expression and then press Enter: \n");
                inputExpr = Console.ReadLine();

                double result = 0.0;

                try
                {
                    result = calculator.EvaluateExpression(inputExpr);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}