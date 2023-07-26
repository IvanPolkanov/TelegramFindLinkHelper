using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TelegramFindLinkHelper.Models;

namespace TelegramFindLinkHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var str = new StringBuilder();

            foreach (var item in args)
            {
                str.Append(item);
            }

            Console.WriteLine($"Your args are : \n\t\t\t {str.ToString()}");

            //var exists = File.Exists(filePath);

            //if (!exists)
            //{
            //    Console.WriteLine("File has not been founded:");
            //    continue;
            //}

            //var jsonContent = File.ReadAllText(filePath);

            //var chatData = JsonSerializer.Deserialize<TelegramJSONChatExport>(jsonContent);
            //var messangesWithDate = chatData.messages.Select(x => (x.date, x.text_entities?.FirstOrDefault()?.text)).Where(x => x.text != null).ToArray();

            //var messangesWithDateHttp = messangesWithDate.Where(x => x.text.StartsWith("http")).ToArray();

            Console.ReadLine();
        }
    }


}
