using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BitContainer bc = new BitContainer();
            bc.pushBit(1);
            bc.pushBit(0);
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            bc[1] = 1;
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            bc.pushBit(false);
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            for(int i = 0; i < 16; ++i)
            {
                bc.pushBit(i % 2);
            }
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            bc[8] = 0;
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            bc.Remove(8);
            Console.WriteLine("val {0} len {1}", bc, bc.Length);
            bc.Insert(8, 0);
            Console.WriteLine("val {0} len {1}", bc, bc.Length);

            foreach (bool el in bc)
            {
                Console.Write("{0} ", el);
            }

            Console.ReadKey();
        }
    }
}
