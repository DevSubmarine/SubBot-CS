using Discord.Commands;

namespace DevSubmarine.SubBot
{
    public class CommandOptions
    {
        public string Prefix { get; set; } = "++";
        public bool AcceptMentionPrefix { get; set; } = true;
        public bool AcceptBotMessages { get; set; } = false;

        public bool CaseSensitive { get; set; } = false;
        public RunMode DefaultRunMode { get; set; } = RunMode.Default;
        public bool IgnoreExtraArgs { get; set; } = true;
    }
}
