using System;

using Microsoft.Extensions.Configuration;

using LoggingConstants = R5T.NetStandard.Logging.Constants;


namespace R5T.NetStandard.Startup
{
    public static class Utilities
    {
        /// <summary>
        /// Gets the configuration key for the startup log file path value.
        /// </summary>
        public static string GetStartupLogFilePathConfigurationKey()
        {
            var output = ConfigurationPath.Combine((string)Logging.Constants.ConfigurationLoggingSectionPath, Constants.StartupLogFilePathKeyName);
            return output;
        }
    }
}
