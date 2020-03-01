using System;
namespace FractionCalculator.Lib
{
    public interface IStringExpressionEvalutor
    {
        string Eval(string[] expression);
    }
}
