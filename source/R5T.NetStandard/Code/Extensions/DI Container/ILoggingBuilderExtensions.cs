using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using LoggingConstants = R5T.NetStandard.Logging.Constants;


namespace R5T.NetStandard
{
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddConfiguration(this ILoggingBuilder loggingBuilder, IConfiguration configuration, ILogger logger)
        {
            var configurationName = configuration is IConfigurationSection configurationSection ? configurationSection.Path : @"configuration";

            logger.LogDebug($@"Adding configuration section ""{configurationName}"" to logging.");

            loggingBuilder.AddConfiguration(configuration);

            logger.LogInformation($@"Added configuration section ""{configurationName}"" to logging.");

            return loggingBuilder;
        }

        /// <summary>
        /// Adds the Logging (<see cref="LoggingConstants.ConfigurationLoggingSectionPath"/>) section as a configuration to the logging builder.
        /// </summary>
        public static ILoggingBuilder AddLoggingConfigurationSection(this ILoggingBuilder loggingBuilder, IConfiguration configuration, ILogger logger)
        {
            loggingBuilder.AddConfiguration(configuration.GetSection(LoggingConstants.ConfigurationLoggingSectionPath), logger);

            return loggingBuilder;
        }

        public static ILoggingBuilder AddLoggingConfigurationSection(this ILoggingBuilder loggingBuilder, ILogger logger)
        {
            var configuration = loggingBuilder.Services.GetConfiguration();

            loggingBuilder.AddLoggingConfigurationSection(configuration, logger);

            return loggingBuilder;
        }
    }
}
