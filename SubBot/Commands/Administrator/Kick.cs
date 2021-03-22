using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DevSubmarine.SubBot.Commands.Administrator
{
    public class Kick : ModuleBase<SocketCommandContext>
    {
        [RequireOwner]
        [RequireContext(ContextType.Guild, ErrorMessage = "Please use this command in a server!")]
        [RequireUserPermission(GuildPermission.KickMembers, ErrorMessage = "You do not have permission: `Kick Members`")]
        [Command("kick")]
        public async Task KickUser(IGuildUser user = null, string reason = "Not specified!")
        {
            if (user == null)
            {
                Embed nullUser = new EmbedBuilder()
                    .WithColor(Color.Red)
                    .WithTitle("Kick Command : Info")
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

                    Embed successKick = new EmbedBuilder()
                        .WithColor(Color.Green)
                        .WithAuthor("Kick Command : Success", user.GetAvatarUrl())
                        .WithDescription($"Successfully kicked {user.Mention}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: successKick);
                }
                catch (Exception ex)
                {
                    Embed errorKick = new EmbedBuilder()

                        .WithColor(Color.Red)
                        .WithTitle("Kick Command : Error")
                        .WithDescription(ex.Message)
                        .WithFooter($"{ex.HResult}")
                        .Build();
                    await Context.Message.Channel.SendMessageAsync(embed: errorKick);
                }
            }
        }
    }
}
