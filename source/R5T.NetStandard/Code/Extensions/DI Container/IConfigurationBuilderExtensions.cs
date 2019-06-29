using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using R5T.NetStandard.IO.Paths;
using R5T.NetStandard.Logging;


namespace R5T.NetStandard
{
    public static class IConfigurationBuilderExtensions
    {
        public static string GetAppSettingsFileAddedMessage(FilePath appSettingsFilePath, bool optional)
        {
            var optionalMessage = optional ? @" (optional)" : String.Empty;

            var output = $@"Application Settings file added. Path: {appSettingsFilePath}{optionalMessage}";
            return output;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, FilePath appSettingsFilePath, bool optional, ILogger logger)
        {
            configurationBuilder.AddJsonFile(appSettingsFilePath.Value, optional);

            var message = IConfigurationBuilderExtensions.GetAppSettingsFileAddedMessage(appSettingsFilePath, optional);
            logger.LogInformation(message);

            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, FilePath appSettingsFilePath, ILogger logger)
        {
            configurationBuilder.AddAppSettingsFile(appSettingsFilePath, false, logger);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, FilePath appSettingsFilePath)
        {
            configurationBuilder.AddAppSettingsFile(appSettingsFilePath, DummyLogger.Instance);
            return configurationBuilder;
        }

        /// <summary>
        /// Adds the default appsettings.json file.
        /// </summary>
        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            var appSettingsFilePath = AppSettings.GetDefaultFilePath();

            configurationBuilder.AddAppSettingsFile(appSettingsFilePath, logger);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, bool optional, ILogger logger)
        {
            var appSettingsFilePath = AppSettings.GetDefaultFilePath();

            configurationBuilder.AddAppSettingsFile(appSettingsFilePath, optional, logger);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddAppSettingsFile(DummyLogger.Instance);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAppSettingsFile(this IConfigurationBuilder configurationBuilder, bool optional)
        {
            var appSettingsFilePath = AppSettings.GetDefaultFilePath();

            configurationBuilder.AddAppSettingsFile(appSettingsFilePath, optional, DummyLogger.Instance);
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddAspNetCoreEnvironmentSpecificAppSettingsFile(this IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            var aspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();
            if (aspNetCoreEnvironment != AspNetCoreEnvironmentValue.None)
            {
                var aspNetCoreSpecificAppSettingsJsonFilePath = AppSettings.GetFilePathForEnvironment();

                configurationBuilder.AddAppSettingsFile(aspNetCoreSpecificAppSettingsJsonFilePath, true, logger);
            }

            return configurationBuilder;
        }

        public static IConfigurationBuilder LogConfigurationBuilderConfigurationStart(this IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            logger.LogInformationEmphasis(@"Start of Configuration configuration.");
            return configurationBuilder;
        }

        public static IConfiguration Build(this IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            var output = configurationBuilder.Build();

            logger.LogBuiltConfiguration();

            return output;
        }
    }
}
