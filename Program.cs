// Copyright (c) 2024 RFull Development
// This source code is managed under the MIT license. See LICENSE in the project root.
using MatrixRotator;
using System.Diagnostics;
using System.Text;

if (string.IsNullOrEmpty(StartupParameter.InputPath) ||
    string.IsNullOrEmpty(StartupParameter.OutputPath))
{
    Console.Error.WriteLine("Usage:");
    Console.Error.WriteLine("  MatrixRotator <--input|-i> input path <--output|-o> output path");
    return;
}

Stopwatch stopwatch = new();
stopwatch.Start();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var encoding = Encoding.GetEncoding("shift_jis");
Editor editor = new();
using StreamReader reader = new(StartupParameter.InputPath, encoding);
editor.Load(reader);
editor.Rotate();
using StreamWriter writer = new(StartupParameter.OutputPath, false, encoding);
editor.Save(writer);

stopwatch.Stop();
double processingTime = stopwatch.Elapsed.TotalSeconds;
Console.Out.WriteLine($"Done(processing time: {processingTime} seconds)");
