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
            List<string> strings = new List<string>();

            strings.Add("string0");
            strings.Add("string1");
            strings.Add("string2");

            strings[1] = "lol1";
            System.Console.WriteLine("---");
            strings.ForEach(s => System.Console.WriteLine(s));

            strings[2] = "hej";
            System.Console.WriteLine("---");
            strings.ForEach(s => System.Console.WriteLine(s));
        }
    }
}
