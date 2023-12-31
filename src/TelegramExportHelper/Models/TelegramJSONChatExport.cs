﻿using System;

namespace TelegramFindLinkHelper.Models;

public class TelegramJSONChatExport
{
    public string type { get; set; }
    public int id { get; set; }
    public Message[] messages { get; set; }
}

public class Message
{
    public int id { get; set; }
    public string type { get; set; }
    public DateTime date { get; set; }
    public string date_unixtime { get; set; }
    public string from { get; set; }
    public string from_id { get; set; }
    public string forwarded_from { get; set; }
    public string saved_from { get; set; }
    public object text { get; set; }
    public Text_Entities[] text_entities { get; set; }
    public string photo { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string file { get; set; }
    public string thumbnail { get; set; }
    public string mime_type { get; set; }
    public string media_type { get; set; }
    public string sticker_emoji { get; set; }
    public DateTime edited { get; set; }
    public string edited_unixtime { get; set; }
    public Location_Information location_information { get; set; }
    public int duration_seconds { get; set; }
}

public class Location_Information
{
    public float latitude { get; set; }
    public float longitude { get; set; }
}

public class Text_Entities
{
    public string type { get; set; }
    public string text { get; set; }
    public string href { get; set; }
}

