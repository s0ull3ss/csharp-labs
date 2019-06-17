using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace text_processing
{

    class TextProcessor
    {
        public delegate String Oper(String str);

        /*
        private String text;
        private Regex re;
        private Oper op;

        public TextProcessor(ref string text, Regex re, Oper op)
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.re = re ?? throw new ArgumentNullException(nameof(re));
            this.op = op ?? throw new ArgumentNullException(nameof(op));
        }
        */

        private static String UpWord(String str)
        {
            if (str.Length >= 10)
            {
                return str.ToUpper();
            }
            else
            {
                return str;
            }
        }

        private static String UpFirstLetter(String str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(str.Substring(0, 1).ToUpper());
            sb.Append(str.Substring(1));
            return sb.ToString();
        }

        public static String WordsToUpperCase(String str)
        {
            //get words
            Regex re = new Regex(@"(\b[^\s]+\b)");
            return doOperations(str, re, UpWord);
        }

        public static String WordsToUpFirstLetter(String str)
        {
            //get words
            Regex re = new Regex(@"(\b[^\s]+\b)");
            return doOperations(str, re, UpFirstLetter);
        }

        public static String doOperations(String text, Regex re, Oper op)
        {
            MatchCollection matches = re.Matches(text);
            StringBuilder sb = new StringBuilder();

            int ind1 = 0;
            int ind2 = 0;

            foreach(Match match in matches)
            {
                //for debug
                //Console.WriteLine(match.Value);
                String tmp = op(match.Value);

                ind2 = match.Index;

                sb.Append(text.Substring(ind1, ind2 - ind1));
                sb.Append(tmp);

                ind1 = match.Index + match.Length;
            }
            sb.Append(text.Substring(ind1));
            return sb.ToString();
        }
    }
}
