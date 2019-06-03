using System;

namespace RatioLibrary
{
    public class DenominatorException : ArgumentException
    {
        public DenominatorException(string message) : base(message) { }
    }

    public class Ratio
    {
        private int numerator { get; set; }
        private int denominator{ get; set; }

        private static int gcd(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return gcd(b, a % b);
        }

        public Ratio(int numerator, int denominator)
        {
            if(denominator == 0)
            {
                throw new DenominatorException("Denominator == 0");
            }
            int k = gcd(numerator, denominator);
            if (k > 1)
            {
                numerator /= k;
                denominator /= k;
            }
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", numerator, denominator);
        }

        public double ToDouble()
        {
            if (denominator == 0)
            {
                throw new DenominatorException("The denominator should not be zero");
            }
            return (double)numerator / denominator;
        }
    }
}
