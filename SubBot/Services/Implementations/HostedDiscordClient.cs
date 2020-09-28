using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Net.WebSockets;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevSubmarine.SubBot.Services
{
    public class HostedDiscordClient : IHostedDiscordClient, IHostedService, IDisposable
    {
        private readonly ILogger _log;
        private readonly IOptionsMonitor<DiscordOptions> _discordOptions;
        private DiscordSocketClient _client;

        public HostedDiscordClient(IOptionsMonitor<DiscordOptions> discordOptions, ILogger<HostedDiscordClient> log)
        {
            this._discordOptions = discordOptions;
            this._log = log;

            DiscordSocketConfig clientConfig = new DiscordSocketConfig();
            clientConfig.WebSocketProvider = DefaultWebSocketProvider.Instance;
            _client = new DiscordSocketClient(clientConfig);

            _discordOptions.OnChange(async _ =>
            {
                if (_client.ConnectionState == ConnectionState.Connected || _client.ConnectionState == ConnectionState.Connecting)
                {
                    _log.LogInformation("Options changed, reconnecting client");
                    await StopDiscordClientAsync();
                    await StartDiscordClientAsync();
                }
            });
        }

        private async Task StartDiscordClientAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _discordOptions.CurrentValue.BotToken);
            await _client.StartAsync();
        }

        private async Task StopDiscordClientAsync()
        {
            if (_client.LoginState == LoginState.LoggedIn || _client.LoginState == LoginState.LoggingIn)
                await _client.LogoutAsync();
            if (_client.ConnectionState == ConnectionState.Connected || _client.ConnectionState == ConnectionState.Connecting)
                await _client.StopAsync();
        }

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            if (_discordOptions.CurrentValue.AutoConnectGateway)
                return StartDiscordClientAsync();
            else
                return Task.CompletedTask;
        }

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            await StopDiscordClientAsync();
            Dispose();
        }

        public static explicit operator DiscordSocketClient(HostedDiscordClient client)
            => client._client;

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
