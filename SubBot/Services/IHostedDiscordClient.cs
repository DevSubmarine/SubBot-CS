using Discord;

namespace DevSubmarine.SubBot
{
    public interface IHostedDiscordClient
    {
        IDiscordClient Client { get; }
    }
}
