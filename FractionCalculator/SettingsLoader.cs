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

    }
}
