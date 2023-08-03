using System;
using TelegramFindLinkHelper.Models;

namespace TelegramExportHelper.Models;

public sealed class TelegramJSONChatsExport
{
    public string about { get; set; }
    public Chats chats { get; set; }
}

public class Chats
{
    public string about { get; set; }
    public List[] list { get; set; }
}

public class List
{
    public Message[] messages { get; set; }
}
