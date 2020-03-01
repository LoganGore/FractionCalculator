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

        // These parameters will presumably be set with values from the Config class, but will
        // use sane defaults nonetheless. 
        public string ToString(string mixedSeparator = "_", string fractionSeparator = "/")
        {
            string fractString = "";

            // Whole number
            if (Math.Abs(num) == Math.Abs(den) || Math.Abs(den) == 1)
            {
                if (num < 0 || den < 0) fractString += "-";

                int whole = Math.Abs(num) / Math.Abs(den);

                fractString += whole.ToString();
            }
            // Mixed number
            else if (Math.Abs(num) > Math.Abs(den))
            {
                int whole = Math.Abs(num) / Math.Abs(den);
                int modNum = Math.Abs(num) % Math.Abs(den);

                if (num < 0 || den < 0) fractString += "-";

                // Well-formed mixed number.
                fractString += whole.ToString() + mixedSeparator + modNum.ToString() + fractionSeparator + Math.Abs(den).ToString();

            }
            // Fraction
            else
            {
                // Proper fraction.
                if (num != 0)
                {
                    if (num < 0 || den < 0)
                    {
                        fractString += "-";
                    }

                    fractString += Math.Abs(num).ToString() + fractionSeparator + Math.Abs(den).ToString();
                }

                // Return zero if numerator is zero value.
                else
                {
                    fractString = "0";
                }
            }

            return fractString;
        }
    }
}
