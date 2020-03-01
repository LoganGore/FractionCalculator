using System;
using Microsoft.Extensions.Configuration;

/// <summary>
///
/// Used to capture application settings from the appsettings.json file.
/// 
/// </summary>
namespace FractionCalculator.App
{
    public class SettingsLoader
    {
        static IConfiguration config;

        static SettingsLoader()
        {
            config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        }

        public static string getFractionSeparator()
        {
            return config["fraction_separator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getWholeSeparator()
        {
            return config["whole_separator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getAdditionOperator()
        {
            return config["addition_operator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getSubractionOperator()
        {
            return config["subtraction_operator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getDivisionOperator()
        {
            return config["division_operator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getMultiplicationOperator()
        {
            return config["multiplication_operator"] ?? throw new Exception("App configuration not set.");
        }

        public static string getNegativeCharacter()
        {
            return config["negative_character"] ?? throw new Exception("App configuration not set.");
        }

    }
}
