using System;

namespace TelegramExportHelper.Models;

public sealed class LinkData
{
    public DateTime MessageDate { get; set; }
    public string DomainName { get; set; }
    public string Content { get; set; }
}
