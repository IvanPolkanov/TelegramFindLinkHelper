using CsvHelper;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using TelegramExportHelper;

namespace TelegramFindLinkHelper;

internal class Program
{
    static void Main(string[] args)
    {


        string chatContent = string.Empty;

#if DEBUG
        chatContent = File.ReadAllText(@"C:\Users\polka\Downloads\TGExport\ChatExport_2023-07-30\result.json");
#endif

#if (!DEBUG)
  //var str = new StringBuilder();

        //foreach (var item in args)
        //{
        //    str.Append(item);
        //}

        //Console.WriteLine($"Your args are : \n\t\t\t {str.ToString()}");
#endif



        var result = TelegramHelper.GetAllHttpLinks(chatContent);

        using (var writer = new StreamWriter(@"C:\Users\polka\Downloads\TGExport\ChatExport_2023-07-30\links.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(result);
        }

        Console.ReadLine();
    }
}


