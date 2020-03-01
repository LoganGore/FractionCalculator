using System;
using System.Linq;

namespace FractionCalculator.Lib
{
    /// <summary>
    ///
    /// Evaluates an expression of Fractions and mathmatical operators.
    /// An instance of an ICalculator<Fraction> is injected via the constructor.
    ///
    /// Encapsulates basic validation and inputs of operators and fractions, which are then
    /// handled by ICalculator.
    /// 
    /// </summary>
    public class FractionExpressionEvaluator : IExpressionEvalutor<Fraction>
    {
        ICalculator<Fraction> calc;
        string mixedSeparator;
        string fractionSeparator;

        public FractionExpressionEvaluator(string mixedSeparator, string fractionSeparator, ICalculator<Fraction> calc)
        {
            this.calc = calc;
            this.mixedSeparator = mixedSeparator;
            this.fractionSeparator = fractionSeparator;
        }

        private Fraction parseFractionInput(string frac)
        {
            Fraction f;

            // Mixed number. 
            if (frac.Contains(mixedSeparator))
            {
                string[] mixedParts = frac.Split(mixedSeparator);

                if (mixedParts.Length != 2) throw new Exception("Malformed mixed number: " + frac);

                int whole = int.Parse(mixedParts[0]);

                string[] fracParts = mixedParts[1].Split(fractionSeparator);

                if (fracParts.Length != 2) throw new Exception("Malformed fraction in mixed number: " + frac);

                int num = int.Parse(fracParts[0]);
                int den = int.Parse(fracParts[1]);

                f = new Fraction(whole, num, den);
            }
            // Fraction.
            else if (frac.Contains(fractionSeparator))
            {
                string[] fracParts = frac.Split(fractionSeparator);

                if (fracParts.Length != 2) throw new Exception("Malformed fraction: " + frac);

                int num = int.Parse(fracParts[0]);
                int den = int.Parse(fracParts[1]);

                f = new Fraction(num, den);
            }
            // Whole number.
            else
            {
                int num = int.Parse(frac);
                f = new Fraction(num, 1);
            }

            return f;
        }

        public Fraction Eval(string[] input)
        {
            // Initial check for input. 
            if (input == null || input.Length == 0) throw new Exception("Input cannot be empty.");

            // Remove white space.
            input = input.Where(a => (!String.IsNullOrWhiteSpace(a))).ToArray();

            Fraction lhs = null;
            string op = null;

            // Parse operators and fractions.
            // This also validates order of operators and fractions is well-formed.
            foreach (string a in input)
            {
                // --lhs --
                // If lhs is null, parse input and set it.
                // This is only true on the first number.
                if (lhs == null)
                {
                    lhs = parseFractionInput(a);
                }
                // -- operator --
                // lhs has value.
                // op is null, meaning we set the op.
                else if (op == null)
                {
                    if ("*-/+".Contains(a))
                    {
                        op = a;
                    }
                    else
                    {
                        throw new Exception("Operator unsupported: " + a);
                    }
                }
                // -- rhs --
                // If we have lhs and op, then we are going to parse the lhs
                // and perform the math. This is the general case.
                else
                {
                    switch (op)
                    {
                        case "+":
                            {
                                lhs = calc.Add(lhs, parseFractionInput(a));
                                break;
                            }
                        case "-":
                            {
                                lhs = calc.Subtract(lhs, parseFractionInput(a));
                                break;
                            }
                        case "*":
                            {
                                lhs = calc.Multiply(lhs, parseFractionInput(a));
                                break;
                            }
                        case "/":
                            {
                                lhs = calc.Divide(lhs, parseFractionInput(a));
                                break;
                            }
                        default:
                            {
                                throw new Exception("Arithmatic error: unknown operator.");
                            }
                    }

                    op = null;
                }

            }

            if (op != null) throw new Exception("Invalid input: trailing operators not supported.");

            return lhs;
        }
    }
}

