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
    public class FractionExpressionEvaluator : IStringExpressionEvalutor
    {
        ICalculator<Fraction> calc;
        readonly string wholeSeparator;
        readonly string fractionSeparator;
        readonly string additionOperator;
        readonly string subtractionOperator;
        readonly string multiplicationOperator;
        readonly string divisionOperator;
        readonly string negativeCharacter;

        public FractionExpressionEvaluator(
            string wholeSeparator,
            string fractionSeparator,
            string additionOperator,
            string subtractionOperator,
            string multiplicationOperator,
            string divisionOperator,
            string negativeCharacter,
            ICalculator<Fraction> calc)
        {
            // Inject calculator.
            this.calc = calc;

            // Supply settings.
            this.wholeSeparator = wholeSeparator;
            this.fractionSeparator = fractionSeparator;
            this.additionOperator = additionOperator;
            this.subtractionOperator = subtractionOperator;
            this.multiplicationOperator = multiplicationOperator;
            this.divisionOperator = divisionOperator;
            this.negativeCharacter = negativeCharacter;
        }

        private Fraction parseString(string frac)
        {
            Fraction f;

            // Mixed number. 
            if (frac.Contains(wholeSeparator))
            {
                string[] mixedParts = frac.Split(wholeSeparator);

                if (mixedParts.Length != 2) throw new Exception("Malformed mixed number: " + frac);

                int whole = int.Parse(mixedParts[0]);

                string[] fracParts = mixedParts[1].Split(fractionSeparator);

                if (fracParts.Length != 2) throw new Exception("Malformed fraction in mixed number: " + frac);

                int num = int.Parse(fracParts[0]);
                int den = int.Parse(fracParts[1]);

                f = new Fraction(whole, num, den, wholeSeparator, fractionSeparator);
            }
            // Fraction.
            else if (frac.Contains(fractionSeparator))
            {
                string[] fracParts = frac.Split(fractionSeparator);

                if (fracParts.Length != 2) throw new Exception("Malformed fraction: " + frac);

                int num = int.Parse(fracParts[0]);
                int den = int.Parse(fracParts[1]);

                f = new Fraction(num, den, wholeSeparator, fractionSeparator);
            }
            // Whole number.
            else
            {
                int num = int.Parse(frac);
                f = new Fraction(num, 1);
            }

            return f;
        }

        /// <summary>
        /// Converts a Fraction instance to a string.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private string fractionToString(Fraction f)
        {
            string fractString = String.Empty;

            int den = f.getDenomenator();
            int num = f.getNumerator();

            // Whole number
            if (Math.Abs(num) == Math.Abs(den) || Math.Abs(den) == 1)
            {
                if (num < 0 || den < 0) fractString += negativeCharacter;

                int whole = Math.Abs(num) / Math.Abs(den);

                fractString += whole.ToString();
            }
            // Mixed number
            else if (Math.Abs(num) > Math.Abs(den))
            {
                int whole = Math.Abs(num) / Math.Abs(den);
                int modNum = Math.Abs(num) % Math.Abs(den);

                if (num < 0 || den < 0) fractString += negativeCharacter;

                // Well-formed mixed number.
                fractString += whole.ToString() + wholeSeparator + modNum.ToString() + fractionSeparator + Math.Abs(den).ToString();

            }
            // Fraction
            else
            {
                // Proper fraction.
                if (num != 0)
                {
                    if (num < 0 || den < 0)
                    {
                        fractString += negativeCharacter;
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

        // Validates a string operator is valid given supplied operators. 
        private bool isValidOperator(string op)
        {
            if (multiplicationOperator == op) return true;
            else if (divisionOperator == op) return true;
            else if (additionOperator == op) return true;
            else if (subtractionOperator == op) return true;
            else return false;
        }

        /// <summary>
        /// Evaluates a string expression, and interacts with the ICalculator instance to obtain an
        /// answer which is then parsed to a string and returned to the caller. This function provides
        /// logical buffering between the input/output and the calculator operations.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Eval(string[] input)
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
                    lhs = parseString(a);
                }
                // -- operator --
                // lhs has value.
                // op is null, meaning we set the op.
                else if (op == null)
                {
                    if (isValidOperator(a))
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
                    if (op == additionOperator) {
                        lhs = calc.Add(lhs, parseString(a));
                    }
                    else if(op == subtractionOperator)
                    {
                        lhs = calc.Subtract(lhs, parseString(a));
                    }
                    else if(op == multiplicationOperator)
                    {
                        lhs = calc.Multiply(lhs, parseString(a));
                    }
                    else if(op==divisionOperator)
                    {
                        lhs = calc.Divide(lhs, parseString(a));
                    }
                    else
                    { 
                        throw new Exception("Arithmatic error: unknown operator."); 
                    }

                    op = null;
                }

            }

            if (op != null) throw new Exception("Invalid input: trailing operators not supported.");

            return fractionToString(lhs);
        }
    }
}

