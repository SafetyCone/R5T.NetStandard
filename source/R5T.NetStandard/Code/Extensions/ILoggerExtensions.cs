using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard
{
    public static class ILoggerExtensions
    {
        public static void LogBuiltConfiguration(this ILogger log)
        {
            log.LogInformationEmphasis(@"Built configuration.");
        }
    }
}
