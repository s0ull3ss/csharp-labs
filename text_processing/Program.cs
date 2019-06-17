using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace text_processing
{
    class Program
    {
        private static String UpOrQuotes(String str)
        {
            if (str.Length >= 5)
            {
                return str.ToUpper();
            }
            else
            {
                return '"' + str + '"';
            }
        }

        static void Main(string[] args)
        {
            String res;

            string text = "hi, i am abcde evil qwerty qweqweqweqwe";
            Regex re = new Regex(@"(\b[^\s]+\b)");

            //string text = "{hi, i am abcde evil qwerty qweqweqweqwe} and {3} not in {here} and {3}";
            //Regex re = new Regex(@"{.+?}");

            res = TextProcessor.doOperations(text, re, TextProcessor.WordsToUpperCase);
            Console.WriteLine(res);

            res = TextProcessor.doOperations(text, re, TextProcessor.WordsToUpFirstLetter);
            Console.WriteLine(res);

            TextProcessor.Oper oper = UpOrQuotes;
            res = TextProcessor.doOperations(text, re, oper);
            Console.WriteLine(res);

            Console.ReadKey();
        }
    }
}
