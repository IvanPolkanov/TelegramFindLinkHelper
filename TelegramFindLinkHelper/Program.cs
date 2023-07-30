using CommandLine;
using CsvHelper;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using TelegramExportHelper;

namespace TelegramFindLinkHelper;

internal class Program
{
    [Verb("parse", HelpText = "Parse data from telegram data export")]
    private sealed class ParseOptions
    {
        [Option('f', "file", Required = true, HelpText = "Input file path to be processed")]
        public string InputFilePath { get; set; }

        [Option('o', "output",
                Default = "",
                HelpText = "Output file path")]
        public string OutputFilePath { get; set; }
    }

    static void Main(string[] args)
    {
        string chatContent = string.Empty;

#if DEBUG
        var options = new ParseOptions()
        {
            InputFilePath = @"C:\TestData\TGExport\result.json"
            //OutputFilePath = "C:\\TestData\\TGExport"
        };

        RunParseTelegramDataJsonExport(options);
#endif

#if (!DEBUG)
        var results = CommandLine.Parser.Default.ParseArguments<ParseOptions>(args)
     .MapResult(
       (ParseOptions opts) => RunParseTelegramDataJsonExport(opts),
       errs => 1);
#endif        
    }

    private static int RunParseTelegramDataJsonExport(ParseOptions parseOptions)
    {
        if (!File.Exists(parseOptions.InputFilePath))
        {
            Console.WriteLine("Specified file has not been found. Please check input file path");
            return -1;
        }

        var outputPath = parseOptions.OutputFilePath;

        if (string.IsNullOrEmpty(parseOptions.OutputFilePath) || !Directory.Exists(parseOptions.OutputFilePath))
        {
            //var startupPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var startupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);


            Console.WriteLine($"-----------   {startupPath}");

            var exportDirectory = $"{startupPath}\\Export";

            if (!Directory.Exists(exportDirectory))
                Directory.CreateDirectory(exportDirectory);

            outputPath = exportDirectory;
        }

        outputPath = $"{outputPath}\\export.csv";

        GetAllDataAndSave(parseOptions.InputFilePath, outputPath);

        return 0;
    }

    private static void GetAllDataAndSave(string inputFilePath, string outputFilePath)
    {
        var chatContent = File.ReadAllText(inputFilePath);

        var result = TelegramHelper.GetAllHttpLinks(chatContent);

        Console.WriteLine($"Save file to \n {outputFilePath} \n");

        using (var writer = new StreamWriter(outputFilePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(result);
        }
    }
}


