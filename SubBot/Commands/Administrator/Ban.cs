using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DevSubmarine.SubBot.Commands.Administrator
{
    public class Ban : ModuleBase<SocketCommandContext>
    {
        [RequireOwner]
        [RequireContext(ContextType.Guild, ErrorMessage = "Please use this command in a server!")]
        [RequireBotPermission(GuildPermission.BanMembers, ErrorMessage = "I do not have the permission to `Ban Members`")]
        [RequireBotPermission(GuildPermission.BanMembers, ErrorMessage = "You do not have the permission to `Ban Members`")]
        [Command("ban")]
        public async Task BanUser(IGuildUser user = null, string reason = "Not specified!", [Remainder] int pruneDays = 0)
        {
            if (user == null)
            {
                Embed nullUser = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Ban Command : Info")
                    .AddField("Usage:", "`ban <user> <reason*> <pruneMessages* (days)>`", true)
                    .WithDescription("Bans a user from this server.")
                    .WithFooter("*Optional parameters")
                    .Build();
                await Context.Message.Channel.SendMessageAsync(embed: nullUser);
            }

            else
            {
                try
                {
                    await user.BanAsync(pruneDays, reason);

                    Embed successBan = new EmbedBuilder()
                        .WithColor(Color.Green)
                        .WithAuthor("Ban Command : Success", user.GetAvatarUrl())
                        .WithDescription($"Successfully banned {user.Mention}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: successBan);
                }
                catch (Exception ex)
                {
                    Embed errorBan = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("Ban Command : Error")
                        .WithDescription(ex.Message)
                        .WithFooter($"{ex.HResult}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: errorBan);
                }
            }
        }
    }
}
