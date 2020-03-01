using System;
using FractionCalculator.Lib;

namespace FractionCalculator.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                FractionExpressionEvaluator evaluator = new FractionExpressionEvaluator(
                    SettingsLoader.getWholeSeparator(),
                    SettingsLoader.getFractionSeparator(),
                    SettingsLoader.getAdditionOperator(),
                    SettingsLoader.getSubractionOperator(),
                    SettingsLoader.getMultiplicationOperator(),
                    SettingsLoader.getDivisionOperator(),
                    SettingsLoader.getNegativeCharacter(),
                    new FractionCalculator.Lib.FractionCalculator()
                ) ;

                //
                // Allow CLI argument-based operation, or interactive console.
                //
                if (args!=null && args.Length > 0)
                {
                    try
                    {
                        Console.WriteLine(evaluator.Eval(args));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        Console.WriteLine("Please enter an expression: ");
                        string exp = Console.ReadLine();

                        if (String.IsNullOrWhiteSpace(exp))
                        {
                            throw new FormatException("Input cannot be empty.");
                        }

                        string[] consoleArgs = exp.Split(" ");

                        Console.WriteLine(evaluator.Eval(consoleArgs));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }

                    Main(null);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled Error: " + ex.Message);
            }
        }
    }
}