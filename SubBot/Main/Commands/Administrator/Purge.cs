using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevSubmarine.SubBot.Commands.Administrator
{
    public class Purge : ModuleBase<SocketCommandContext>
    {
        [Command("purge"), Alias("delete")]
        public async Task PurgeMessages(int? msgToDel = null)
        {
            if (msgToDel == null || msgToDel == 0)
            {
                Embed NullEmbed = new EmbedBuilder()
                    .WithColor(Color.Blue)
                    .WithTitle("Purge Messages : Info")
                    .AddField("Info", "Deletes a specified amount of messages")
                    .AddField("To Use", "`purge/delete {NumOfMessages}`")
                    .Build();
                await ReplyAsync(embed: NullEmbed);
            }

            else
            {
                int amount = Convert.ToInt32(msgToDel);
                var messages = await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount).FlattenAsync();
                var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);
                var count = filteredMessages.Count();

                if (count < 0 || count > 101)
                {
                    Embed errorPurge = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("Purge Messages : Error")
                        .WithDescription("You cannot delete less than 0 and more than 100 messages!")
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: errorPurge);
                }

                else
                {
                    try
                    {
                        await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);

                        Embed Success = new EmbedBuilder()
                            .WithColor(Color.Green)
                            .WithTitle("Purge Messages : Success")
                            .WithDescription($"Done. Removed {count} {(count > 1 ? "messages" : "message")}.")
                            .Build();
                        await Context.Message.Channel.SendMessageAsync(embed: Success);
                    }

                    catch (Exception ex)
                    {
                        Embed errorSlowmode = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("Purge Messages : Error")
                        .WithDescription(ex.Message)
                        .WithFooter($"{ex.HResult}")
                        .Build();
                        await Context.Message.Channel.SendMessageAsync(embed: errorSlowmode);
                    }
                }
            }
        }
    }
}
