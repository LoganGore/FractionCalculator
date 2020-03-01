using System;
namespace FractionCalculator.Lib
{
    public interface IExpressionEvalutor<T>
    {
        T Eval(string[] expression);
    }
}
