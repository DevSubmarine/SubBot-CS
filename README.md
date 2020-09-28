# SubBot-CS
[![GitHub top language](https://img.shields.io/github/languages/top/DevSubmarine/SubBot-CS)](https://github.com/DevSubmarine/SubBot-CS) [![GitHub](https://img.shields.io/github/license/DevSubmarine/SubBot-CS)](LICENSE) [![GitHub Workflow Status](https://img.shields.io/github/workflow/status/DevSubmarine/SubBot-CS/.NET%20Core%20Build)](https://github.com/DevSubmarine/SubBot-CS/actions) [![GitHub issues](https://img.shields.io/github/issues/DevSubmarine/SubBot-CS)](https://github.com/DevSubmarine/SubBot-CS/issues)

C# instance of SubBot

## Running
1. Download or clone.
2. Add `appsecrets.json` file (with "Copy always" or "Copy if newer" for **Copy to Output Directory**).
3. Populate with secrets. See [appsecrets-example.json](appsecrets-example.json) for example.
4. Build and run.

## Usage
### Accessing Discord client
Currently the bot uses a [DiscordSocketClient](https://discord.foxbot.me/docs/api/Discord.WebSocket.DiscordSocketClient.html) wrapped into [IHostedDiscordClient](SubBot/Services/IHostedDiscordClient.cs) to allow use with .NET Generic Host DI container. To access the client, simply use Constructor Injection in your service.  
Injecting of [DiscordSocketClient](https://discord.foxbot.me/docs/api/Discord.WebSocket.DiscordSocketClient.html) or [IDiscordClient](https://discord.foxbot.me/docs/api/Discord.IDiscordClient.html) instead of [IHostedDiscordClient](SubBot/Services/IHostedDiscordClient.cs) is also supported to provide easier use of the client - each of these injections will return the same instance of the Discord client.

## License
Copyright (c) 2020 DevSubmarine

Licensed under [Apache 2.0 License](LICENSE).