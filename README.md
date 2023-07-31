# Telegram Find Link Helper

Wanna parse all links from any telegram dialog. Right place.

## Description

Just parses all available links from the exported telegram dialog.

## Getting Started

### Installing

No install. Just use CLI tools or download nuget package to use in you own project

### Executing program via CLI

```
tflh.exe -f "your file path here"
```

### Use via nuget packet

```csharp
  using TelegramExportHelper;

  //chatContent - you chat export from telegram
  var result = TelegramHelper.GetAllHttpLinks(chatContent);
```

## Authors

Contributors names and contact info

ex. [Ivan Polkanov](https://duck4duck.ru/about/)

## License

This project is licensed under the [MIT ] License - see the LICENSE.md file for details
