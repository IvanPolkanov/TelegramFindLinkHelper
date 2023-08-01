using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TelegramExportHelper.Models;
using TelegramFindLinkHelper.Models;

namespace TelegramExportHelper;

public class TelegramHelper
{
    /// <summary>
    /// Parse all links from exported dialog
    /// </summary>
    /// <param name="chatContent"></param>
    /// <returns></returns>
    public static IEnumerable<LinkData> GetAllHttpLinks(string chatContent)
    {
        var chatData = JsonSerializer.Deserialize<TelegramJSONChatExport>(chatContent);

        var links = new List<LinkData>();

        foreach (var message in chatData.messages)
        {
            if (message.text_entities == null)
                continue;

            var tempLinks = message.text_entities.Where(x => x.type == "link").ToArray();

            if (!tempLinks.Any())
                continue;

            var date = message.date;


            links.AddRange(tempLinks.Select(x => new LinkData() { MessageDate = date, Content = x.text }).ToArray());
        }

        return links.OrderBy(x => x.MessageDate).
                  ToArray();
    }
}