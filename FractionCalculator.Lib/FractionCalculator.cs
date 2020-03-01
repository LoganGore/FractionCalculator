using System;
namespace FractionCalculator.Lib
{
    /// <summary>
    ///
    /// Handles basic fraction math. Implements ICalculator. 
    /// 
    /// </summary>
    public class FractionCalculator : ICalculator<Fraction>
    {
        public Fraction Add(Fraction x, Fraction y)
        {
            if (y.getDenomenator() == x.getDenomenator())
            {
                return new Fraction(y.getNumerator() + x.getNumerator(), y.getDenomenator());
            }
            else
            {
                return new Fraction(y.getNumerator() * x.getDenomenator() + x.getNumerator() * y.getDenomenator(), y.getDenomenator() * x.getDenomenator());
            }
        }

        public Fraction Subtract(Fraction x, Fraction y)
        {
            if (y.getDenomenator() == x.getDenomenator())
            {
                return new Fraction(x.getNumerator() - y.getNumerator(), y.getDenomenator());
            }
            else
            {
                return new Fraction(x.getNumerator() * y.getDenomenator() - y.getNumerator() * x.getDenomenator(), y.getDenomenator() * x.getDenomenator());
            }
        }

        public Fraction Multiply(Fraction x, Fraction y)
        {
            return new Fraction(y.getNumerator() * x.getNumerator(), y.getDenomenator() * x.getDenomenator());
        }

        public Fraction Divide(Fraction x, Fraction y)
        {
            return new Fraction(y.getDenomenator() * x.getNumerator(), y.getNumerator() * x.getDenomenator());
        }
    }
}
