using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter _writer;

        public Calculator()
        {
            //creates directory if it doesn't exist
            Directory.CreateDirectory("CalculatorLogs");
            //generates unique file name using current date and time
            string dateTime = DateTime.Now.ToString("dd'-'MM'-'yy'_'HH'-'mm'-'ss");
            string fileName = "CalculatorLogs/CalcLog_" + dateTime + ".json";
            StreamWriter logFile = File.CreateText(fileName);
            logFile.AutoFlush = true;

            _writer = new JsonTextWriter(logFile);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartObject();
            _writer.WritePropertyName("Operations");
            _writer.WriteStartArray();

        }

        public double EvaluateExpression(string expression)
        {
            //Parse and evaluate the mathematical expression
            double result = Parser.Parse(expression).Eval();

            //If no errors occured, write log to the json file
            _writer.WriteStartObject();
            _writer.WritePropertyName("Expression");
            _writer.WriteValue(expression);

            _writer.WritePropertyName("Result");
            _writer.WriteValue(result);
            _writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            _writer.WriteEndArray();
            _writer.WriteEndObject();
            _writer.Close();
        }
    }
}