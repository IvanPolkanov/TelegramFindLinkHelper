using CommandLine;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using TelegramExportHelper;
using TelegramExportHelper.Models;

namespace TelegramFindLinkHelper;

internal class Program
{
    [Verb("parse", HelpText = "Parse data from telegram data export")]
    private sealed class ParseOptions
    {
        [Option('f', "file", Required = true, HelpText = "Input file path to be processed")]
        public string InputFilePath { get; set; }

        [Option('s', "single", HelpText = "Parse single chat export or collection")]
        public bool Single { get; set; }

        [Option('o', "output",
                HelpText = "Output file path")]
        public string OutputFilePath { get; set; }
    }

    static void Main(string[] args)
    {
#if DEBUG
        DebugInteractiveMode();
#endif

#if (!DEBUG)
        var results = CommandLine.Parser.Default.ParseArguments<ParseOptions>(args)
     .MapResult(
       (ParseOptions opts) => RunParseTelegramDataJsonExport(opts),
       errs => 1);
#endif        
    }

    private static void DebugInteractiveMode()
    {
        var single = false;
        ParseOptions options;

        if (single)
        {
            options = new ParseOptions()
            {
                InputFilePath = @"C:\TestData\TGExport\result.json",
                OutputFilePath = "C:\\TestData\\TGExport",
                Single = single,
            };
        }
        else
        {
            options = new ParseOptions()
            {
                InputFilePath = @"C:\TestData\TGExport\AllChats\result.json",
                OutputFilePath = @"C:\TestData\TGExport\AllChats",
                Single = single,
            };
        }

        RunParseTelegramDataJsonExport(options);
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

            var exportDirectory = $"{startupPath}\\Export";

            if (!Directory.Exists(exportDirectory))
                Directory.CreateDirectory(exportDirectory);

            outputPath = exportDirectory;
        }

        outputPath = $"{outputPath}\\export.csv";

        GetAllDataAndSave(parseOptions.InputFilePath, outputPath, parseOptions.Single);

        return 0;
    }

    private static void GetAllDataAndSave(string inputFilePath, string outputFilePath, bool single)
    {
        var rawContent = File.ReadAllText(inputFilePath);

        IEnumerable<LinkData> result;

        if (single)
            result = TelegramHelper.GetAllHttpLinksFromSingleChatExport(rawContent);
        else
            result = TelegramHelper.GetAllHttpLinksFromAllChatsExport(rawContent);

        using (var writer = new StreamWriter(outputFilePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(result);
        }
    }
}


