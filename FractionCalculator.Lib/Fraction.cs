using System;
namespace FractionCalculator.Lib
{
    public class Fraction
    {
        private int num;
        private int den;

        public Fraction(int numerator, int denominator)
        {
            if (denominator != 0)
            {
                this.num = numerator;
                this.den = denominator;

                simplify();
            }
        }

        // Helper method: Mixed number -> improper fraction.
        public Fraction(int whole, int numerator, int denominator)
        {
            if (denominator != 0)
            {
                this.num = numerator;
                this.den = denominator;

                // Mixed
                if (whole != 0)
                {
                    this.num += Math.Abs(whole * denominator);

                    // Negative
                    if (whole < 0)
                    {
                        this.num *= -1;
                    }
                }
                simplify();
            }
        }

        //
        // Overloaded constructors to inject string separators.
        //
        public Fraction(int numerator, int denominator, string wholeSeparator, string fractionSeparator):this(numerator,denominator)
        {
            this.wholeSeparator = wholeSeparator;
            this.fractionSeparator = fractionSeparator;
        }

        public Fraction(int whole, int numerator, int denominator, string wholeSeparator, string fractionSeparator) : this(whole, numerator, denominator)
        {
            this.wholeSeparator = wholeSeparator;
            this.fractionSeparator = fractionSeparator;
        }

        public int getNumerator()
        {
            return num;
        }

        public int getDenomenator()
        {
            return den;
        }

        void simplify()
        {
            int gcd = findGCD(num, den);
            num /= gcd;
            den /= gcd;
        }

        // GCD via Euclid's.
        public int findGCD(int a, int b)
        {
            int r;

            while (b != 0)
            {
                r = b;
                b = a % b;
                a = r;
            }

            return a;
        }
    }
}
