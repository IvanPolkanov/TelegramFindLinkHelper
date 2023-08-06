using System;
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
    public static IEnumerable<LinkData> GetAllHttpLinksFromSingleChatExport(string chatContent)
    {
        var chatData = JsonSerializer.Deserialize<TelegramJSONChatExport>(chatContent);

        var links = new List<LinkData>();

        foreach (var message in chatData.messages)
        {
            if (message.text_entities == null)
                continue;

            var tempLinks = message.text_entities.Where(x => x.type == "link").ToArray();
            var tempHrefLinks = message.text_entities.Where(x => x.type == "text_link").ToArray();

            if (!tempLinks.Any() && !tempHrefLinks.Any())
                continue;

            var date = message.date;

            links.AddRange(tempLinks.Select(x => new LinkData() { MessageDate = date, Content = x.text, DomainName = GetDomainNameFromUrl(x.text) }).ToArray());
            links.AddRange(tempHrefLinks.Select(x => new LinkData() { MessageDate = date, Content = x.href, DomainName = GetDomainNameFromUrl(x.href) }).ToArray());
        }

        return links.OrderBy(x => x.MessageDate).
                  ToArray();
    }

    /// <summary>
    /// Parse all links from exported dialogs
    /// </summary>
    /// <param name="chatContent"></param>
    /// <returns></returns>
    public static IEnumerable<LinkData> GetAllHttpLinksFromAllChatsExport(string telegramChatsExport)
    {
        var chatsData = JsonSerializer.Deserialize<TelegramJSONChatsExport>(telegramChatsExport);

        var links = new List<LinkData>();

        foreach (var chat in chatsData.chats.list)
        {
            foreach (var message in chat.messages)
            {
                if (message.text_entities == null)
                    continue;

                var tempLinks = message.text_entities.Where(x => x.type == "link").ToArray();
                var tempHrefLinks = message.text_entities.Where(x => x.type == "text_link").ToArray();

                if (!tempLinks.Any() && !tempHrefLinks.Any())
                    continue;

                var date = message.date;

                links.AddRange(tempLinks.Select(x => new LinkData() { MessageDate = date, Content = x.text, DomainName = ExtractDomainNameFromURL(x.text) }).ToArray());
                links.AddRange(tempHrefLinks.Select(x => new LinkData() { MessageDate = date, Content = x.href, DomainName = ExtractDomainNameFromURL(x.href) }).ToArray());
            }
        }

        return links.OrderBy(x => x.MessageDate).
                  ToArray();
    }

    private static string GetDomainNameFromUrl(string url)
    {
        try
        {
            Uri myUri = new Uri(url);
            var host = myUri.Host;
            return host;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static string ExtractDomainNameFromURL(string Url)
    {
        return System.Text.RegularExpressions.Regex.Replace(
            Url,
            @"^([a-zA-Z]+:\/\/)?([^\/]+)\/.*?$",
            "$2"
        );
    }
}