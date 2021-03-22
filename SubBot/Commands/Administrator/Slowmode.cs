using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DevSubmarine.SubBot.Commands.Administrator
{
    public class Slowmode : ModuleBase<SocketCommandContext>
    {
        [Command("slowmode"), Alias("sm")]
        [RequireContext(ContextType.Guild, ErrorMessage = "Please use this command in a server!")]
        [RequireUserPermission(GuildPermission.ManageChannels, ErrorMessage = "You do not have permission `Manage Channels`")]
        public async Task SetSlowmode(int? duration = null)
        {
            if (duration == null)
            {
                var slowmodeInfo = new EmbedBuilder()
                    .WithColor(Color.Blue)
                    .WithTitle("Slowmode Command : Info")
                    .AddField("Info", "Sets the slowmode of text channels in seconds.")
                    .AddField("To Use", "`slowmode <duration>` or `sm <duration>`")
                    .WithFooter("Note: <duration> is in seconds")
                    .Build();

                await Context.Channel.SendMessageAsync(embed: slowmodeInfo);
            }

            else if (duration > 21600) // 21600 = 6 hours
            {
                var maxLimit = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Slowmode Command : Error")
                    .WithDescription("You cannot use more than 21600 (6 hours)!")
                    .Build();

                await Context.Channel.SendMessageAsync(embed: maxLimit);
            }

            else if (duration < 0)
            {
                var minLimit = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Slowmode Command : Error")
                    .WithDescription("You cannot use less than 0!")
                    .Build();

                await Context.Channel.SendMessageAsync(embed: minLimit);
            }

            else if (duration != null && duration > -1 && duration < 21601)
            {
                try
                {
                    string strGuildId = Context.Message.Channel.Id.ToString();
                    ulong id = Convert.ToUInt64(strGuildId);
                    ITextChannel channel = Context.Guild.GetChannel(id) as SocketTextChannel;

                    await channel.ModifyAsync(x => x.SlowModeInterval = Convert.ToInt32(duration));

                    var Result = new EmbedBuilder()
                        .WithColor(Color.Green)
                        .WithTitle("Slowmode Command : Success")
                        .WithDescription($"Slowmode of {duration} seconds was set!")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: Result);
                }

                catch (Exception ex)
                {
                    Embed errorSlowmode = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("Slowmode Command : Error")
                        .WithDescription(ex.Message)
                        .WithFooter($"{ex.HResult}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: errorSlowmode);
                }
            }
        }
    }
}
