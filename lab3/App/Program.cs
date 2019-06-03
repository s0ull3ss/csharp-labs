using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatioLibrary;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Ratio a = new Ratio(2, 4);
            Ratio b = new Ratio(13, 37);
            Console.WriteLine("a: {0} b: {1}", a, b);
            Console.WriteLine("a: {0} b: {1}", a.ToDouble(), b.ToDouble());
            Console.WriteLine("{0} + {1} = {2}", a, b, (a + b));
            Console.WriteLine("{0} - {1} = {2}", a, b, (a - b));
            Console.WriteLine("{0} * {1} = {2}", a, b, (a * b));
            Console.WriteLine("{0} / {1} = {2}", a, b, (a / b));
            Console.ReadKey();
        }
    }
}
