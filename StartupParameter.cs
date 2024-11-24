// Copyright (c) 2024 RFull Development
// This source code is managed under the MIT license. See LICENSE in the project root.
using Microsoft.Extensions.Configuration;

namespace MatrixRotator
{
    public class StartupParameter
    {
        public static string? InputPath { get; }
        public static string? OutputPath { get; }

        static StartupParameter()
        {
            string[] args = Environment.GetCommandLineArgs();
            Dictionary<string, string> optionMap = new()
            {
                { "-i", "input" },
                { "--input", "input" },
                { "-o", "output" },
                { "--output", "output" }
            };
            ConfigurationBuilder builder = new();
            builder.AddCommandLine(args, optionMap);
            var config = builder.Build();

            InputPath = config["input"];
            OutputPath = config["output"];
        }
    }
}
