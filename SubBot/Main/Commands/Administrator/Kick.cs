using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DevSubmarine.SubBot.Commands.Administrator
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [RequireOwner]
        [RequireBotPermission(GuildPermission.BanMembers, ErrorMessage = "I do not have permission: `Ban Members`")]
        [Command("kick")]
        public async Task KickUser(IGuildUser user = null, string reason = "Not specified!")
        {
            if (user == null)
            {
                Embed nullUser = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Kick Command : Error")
                    .AddField("Usage:", "`kick <user> <reason*>`", true)
                    .WithDescription("Kicks a user from this guild.")
                    .WithFooter("*Optional parameters")
                    .Build();
                await Context.Message.Channel.SendMessageAsync(embed: nullUser);
            }

            else
            {
                try
                {
                    await user.KickAsync(reason);

                    Embed successBan = new EmbedBuilder()
                        .WithColor(Color.Green)
                        .WithAuthor("Kick Command : Success", user.GetAvatarUrl())
                        .WithDescription($"Successfully banned {user.Mention}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: successBan);
                }
                catch (Exception ex)
                {
                    Embed errorBan = new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithTitle("Kick Command : Error")
                        .WithDescription(ex.Message)
                        .WithFooter($"{ex.HResult}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: errorBan);
                }
            }
        }
    }
}
