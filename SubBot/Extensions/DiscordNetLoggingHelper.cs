using System;
using System.Collections.Generic;
using Discord;
using Microsoft.Extensions.Logging;

namespace DevSubmarine.SubBot
{
    public static class DiscordNetLoggingHelper
    {
        public static void Log(this ILogger logger, LogMessage message)
        {
            LogLevel level = message.Severity.ToLogLevel();
            if (!logger.IsEnabled(level))
                return;

            using (logger.BeginScope(new Dictionary<string, object>()
            {
                { "Label", $"DiscordNet: {message.Source}" }
            }))
            {
                logger.Log(level, message.Exception, message.Message);
            }
        }

        public static LogLevel ToLogLevel(this LogSeverity severity)
        {
            return severity switch
            {
                LogSeverity.Critical => LogLevel.Critical,
                LogSeverity.Error => LogLevel.Error,
                LogSeverity.Warning => LogLevel.Warning,
                LogSeverity.Info => LogLevel.Information,
                LogSeverity.Debug => LogLevel.Debug,
                LogSeverity.Verbose => LogLevel.Trace,
                _ => throw new ArgumentException($"Unknown severity {severity}", nameof(severity)),
            };
        }

        public static LogSeverity ToLogSeverity(this LogLevel level)
        {
            return level switch
            {
                LogLevel.Critical => LogSeverity.Critical,
                LogLevel.Error => LogSeverity.Error,
                LogLevel.Warning => LogSeverity.Warning,
                LogLevel.Information => LogSeverity.Info,
                LogLevel.Debug => LogSeverity.Debug,
                LogLevel.Trace => LogSeverity.Verbose,
                _ => throw new ArgumentException($"Unknown log level {level}", nameof(level)),
            };
        }
    }
}
