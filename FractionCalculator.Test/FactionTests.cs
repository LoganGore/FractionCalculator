using NUnit.Framework;
using FractionCalculator.Lib;

namespace FractionCalculator.Test
{
    public class FractionCalculatorTests
    {
        FractionExpressionEvaluator evaluator;

        public FractionCalculatorTests()
        {
            evaluator = new FractionExpressionEvaluator("_", "/","+","-","*","/","-",
    new FractionCalculator.Lib.FractionCalculator()
);
        }

        public string calc(string input)
        {
            string[] consoleArgs = input.Split(" ");

            return evaluator.Eval(consoleArgs);
        }

        //
        // Addition
        //

        [Test]
        public void Add_Fractions()
        {
            Assert.AreEqual("1/2", calc("1/4 + 2/8"));
        }

        [Test]
        public void Add_Mixed()
        {
            Assert.AreEqual("2_1/2", calc("1_1/4 + 1_2/8"));
            Assert.AreEqual("2", calc("1_1/4 + 6/8"));
        }

        [Test]
        public void Add_Whole()
        {
            Assert.AreEqual("2", calc("1 + 1"));
        }

        [Test]
        public void Wrong_Whole()
        {
            Assert.AreNotEqual("2", calc("1 + 2"));
        }

        //
        // Subtraction
        //

        [Test]
        public void Sub_Fraction()
        {
            Assert.AreEqual("1/3", calc("2/3 - 2/6"));
        }

        [Test]
        public void Sub_Mixed()
        {
            Assert.AreEqual("5", calc("6_1/2 - 1_2/4"));
        }

        [Test]
        public void Sub_Whole()
        {
            Assert.AreEqual("-2", calc("2 - 4"));
        }

        //
        // Division
        //

        [Test]
        public void Div_Fraction()
        {
            Assert.AreEqual("2", calc("1/2 / 1/4"));
        }

        [Test]
        public void Div_Mixed()
        {
            Assert.AreEqual("4", calc("5 / 1_1/4"));
        }

        [Test]
        public void Div_Whole()
        {
            Assert.AreEqual("8", calc("16 / 2"));
        }

        //
        // Multiplication
        //

        [Test]
        public void Mul_Fraction()
        {
            Assert.AreEqual("1/8", calc("1/2 * 25/100"));
        }

        [Test]
        public void Mul_Mixed()
        {
            Assert.AreEqual("4_7/8", calc("3_4/16 * 1_1/2"));
        }

        [Test]
        public void Mul_Whole()
        {
            Assert.AreEqual("1000", calc("10 * 100"));
        }

        //
        // Bad input
        //

        [Test]
        public void MalformedInput()
        {
            Assert.Throws<System.FormatException>(() => calc("1*1"));
            Assert.Throws<System.FormatException>(() => calc("a;lsdkfj"));
            Assert.Throws<System.NullReferenceException>(() => calc(""));
        }
    }
}
