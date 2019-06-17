using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Startup
{
    public static class ILoggerExtensions
    {
        public static void LogStartupConstructorStart(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"Constructor() start.");
        }

        public static void LogConfigureConfigurationStart(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"ConfigureConfiguration() start.");
        }

        public static void LogConfigureConfigurationEnd(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"ConfigureConfiguration() end.");
        }

        public static void LogConfigureServicesStart(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"ConfigureServices() start.");
        }

        public static void LogConfigureServicesEnd(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"ConfigureServices() end.");
        }

        public static void LogConfigureStart(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"Configure() start.");
        }

        public static void LogConfigureEnd(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"Configure() end.");
        }

        public static void LogTestServicesStart(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"TestServices() start.");
        }

        public static void LogTestServicesEnd(this ILogger startupLog)
        {
            startupLog.LogInformationEmphasis($@"TestServices() end.");
        }
    }
}
