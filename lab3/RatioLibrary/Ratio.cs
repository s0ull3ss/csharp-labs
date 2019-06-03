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

        public static Ratio operator +(Ratio r1, Ratio r2)
        {
            int k = gcd(r1.denominator, r2.denominator);
            return new Ratio(r1.numerator * (r2.denominator / k) + r2.numerator * (r1.denominator / k), r1.denominator * r2.denominator / k);
        }

        public static Ratio operator -(Ratio r1, Ratio r2)
        {
            int k = gcd(r1.denominator, r2.denominator);
            return new Ratio(r1.numerator * (r2.denominator / k) - r2.numerator * (r1.denominator / k), r1.denominator * r2.denominator / k);
        }

        public static Ratio operator +(Ratio r)
        {
            return new Ratio(r.numerator, r.denominator);
        }

        public static Ratio operator -(Ratio r)
        {
            return new Ratio(-r.numerator, r.denominator);
        }

        public static Ratio operator *(Ratio r1, Ratio r2)
        {
            int k1 = gcd(r1.numerator, r2.denominator);
            int k2 = gcd(r2.numerator, r1.denominator);
            return new Ratio(r1.numerator / k1 * r2.numerator / k2, r1.denominator / k2 * r2.denominator / k1);
        }

        public static Ratio operator /(Ratio r1, Ratio r2)
        {
            if (r2.numerator == 0)
            {
                throw new DenominatorException("Division by zero");
            }
            int k1 = gcd(r1.numerator, r2.numerator);
            int k2 = gcd(r1.denominator, r2.denominator);
            return new Ratio(r1.numerator/k1 * r2.denominator/k2, r1.denominator/k2 * r2.numerator/k1);
        }
    }
}
