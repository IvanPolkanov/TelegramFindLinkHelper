[![dotnet package](https://github.com/IvanPolkanov/TelegramFindLinkHelper/actions/workflows/dotnet.yml/badge.svg)](https://github.com/IvanPolkanov/TelegramFindLinkHelper/actions/workflows/dotnet.yml)

# Telegram Find Link Helper

Wanna parse all links from any telegram dialog. Right place.

## Description

Just parses all available links from the exported telegram dialog.

## Installing

No install. Just use CLI tools or download nuget package to use in you own project

## Executing program via CLI

### Show help info
```
tflh.exe -h
```

### Parse single chat export
```
tflh.exe -f "your file path here" -s
```

### Parse chata export from telegram
```
tflh.exe -f "your file path here"
```

## Use via nuget packet

```csharp
  using TelegramExportHelper;

  //chatContent - you chat export from telegram
  var result = TelegramHelper.GetAllHttpLinks(chatContent);
```

## Authors

Contributors names and contact info

ex. [Ivan Polkanov](https://duck4duck.ru/about/)

## License

This project is licensed under the [MIT](https://github.com/IvanPolkanov/TelegramFindLinkHelper/blob/main/LICENSE) License - see the LICENSE.md file for details
