using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingArea
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (int i in TestYield())
            {
                Console.WriteLine("{0} ", i);
            }
        }

        static IEnumerable<int> TestYield()
        {
            System.Console.WriteLine("1");
            yield return 1;

            System.Console.WriteLine("3");
            yield return 3;

            System.Console.WriteLine("4");
            yield return 4;
        }
    }
}
