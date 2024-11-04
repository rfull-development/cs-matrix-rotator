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
    return ExitCode.Failure;
}

Stopwatch stopwatch = new();
stopwatch.Start();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var encoding = Encoding.GetEncoding("shift_jis");
Editor editor = new();

Console.Write($"Inputfile '{StartupParameter.InputPath}' loading ... ");
try
{
    using StreamReader reader = new(StartupParameter.InputPath, encoding);
    editor.Load(reader);
}
catch (Exception e)
{
    Console.Error.WriteLine($"Failed to load the input file: {StartupParameter.InputPath}");
    Console.Error.WriteLine($"Description: {e.Message}");
    return ExitCode.Failure;
}
Console.WriteLine("Done.");

Console.Write("Rotating the matrix ... ");
try
{
    editor.Rotate();
}
catch (Exception e)
{
    Console.Error.WriteLine($"Description: {e.Message}");
    return ExitCode.Failure;
}
Console.WriteLine("Done.");

Console.Write($"Outputfile '{StartupParameter.OutputPath}' saving ... ");
try
{
    using StreamWriter writer = new(StartupParameter.OutputPath, false, encoding);
    editor.Save(writer);
}
catch (Exception e)
{
    Console.Error.WriteLine($"Failed to save the output file: {StartupParameter.OutputPath}");
    Console.Error.WriteLine($"Description: {e.Message}");
    return ExitCode.Failure;
}
Console.WriteLine("Done.");

stopwatch.Stop();
double processingTime = stopwatch.Elapsed.TotalSeconds;
Console.Out.WriteLine($"Done(processing time: {processingTime} seconds)");
return ExitCode.Success;
