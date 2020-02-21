using Nova.Expressions;
using Nova.Runtime;
using Nova.Statements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateExecutionTime(TestFor);

            Console.Read();
        }
        private static void TestFor()
        {
            RuntimeContext context = new RuntimeContext(Environment.CurrentDirectory);
            context.AddHeapValue("a", new Runtime.Values.RuntimeValue(null, 0));

            var st = StatementBuilder.Build(null, "a = a + 1 * a + (2*3*4*5*6)", 0);

            for (int i = 0; i < 1000000; i++)
            {
                st.Compute(context);
            }
        }
        private static void CalculateExecutionTime(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            Console.WriteLine(action.Method.Name + " takes " + stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}
