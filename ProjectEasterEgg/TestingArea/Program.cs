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
            IEnumerator<int> test1 = TestYield();
            while (test1.MoveNext())
            {
                Console.WriteLine("{0} ", test1.Current);
            }
            
        }

        static IEnumerator<int> TestYield()
        {
            foreach (int i in TestYield2()) { yield return i; }

            yield return 3;

            yield return 4;
        }

        static IEnumerable<int> TestYield2()
        {
            yield return 1;

            yield return 2;
        }
    }
}
