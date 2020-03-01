using System;
namespace FractionCalculator.Lib
{
    public interface ICalculator<T>
    {
        T Add(T x, T y);
        T Subtract(T x, T y);
        T Multiply(T x, T y);
        T Divide(T x, T y);
    }
}
