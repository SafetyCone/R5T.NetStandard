using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace R5T.NetStandard
{
    /// <summary>
    /// Contains functionality common to building both console and web applications.
    /// </summary>
    /// <remarks>
    /// Could add a fluent instance-level interface.
    /// </remarks>
    public static class ApplicationBuilder
    {
        public const string ApplicationMustHaveNameMessage = @"Application must have a name! Application name for application builder was null or empty.";


        //public static IServiceProvider UseStartup<TStartup>(string applicationName, string appSettingsFilePath)
        //    where TStartup: class, IApplicationStartup
        //{
        //    ApplicationBuilder.EnsureApplicationName(applicationName);

        //    // Build the startup service provider.
        //    var startupConfiguration = ApplicationBuilder.GetStartupConfiguration(appSettingsFilePath);

        //    var serviceProvider = ApplicationBuilder.UseStartup<TStartup>(startupConfiguration, applicationName);
        //    return serviceProvider;
        //}

        public static IServiceProvider UseStartup<TStartup>(string applicationName, bool appSettingsFileIsOptional = false)
            where TStartup : class, IApplicationStartup
        {
            ApplicationBuilder.EnsureApplicationName(applicationName);

            // Build the startup service provider.
            var startupConfiguration = ApplicationBuilder.GetStartupConfiguration(appSettingsFileIsOptional);

            var serviceProvider = ApplicationBuilder.UseStartup<TStartup>(startupConfiguration, applicationName);
            return serviceProvider;
        }

        //public static IConfiguration GetStartupConfiguration(string appSettingsFilePath)
        //{
        //    var startupConfiguration = new ConfigurationBuilder()
        //        .AddAppSettingsFile(appSettingsFilePath)
        //        .Build();

        //    return startupConfiguration;
        //}

        public static IConfiguration GetStartupConfiguration(bool appSettingsFileIsOptional = false)
        {
            var startupConfiguration = new ConfigurationBuilder()
                .AddAppSettingsFile(appSettingsFileIsOptional)
                .Build();

            return startupConfiguration;
        }

        public static IServiceProvider UseStartup<TStartup>(IConfiguration startupConfiguration, string applicationName)
            where TStartup : class, IApplicationStartup
        {
            var startupServices = ApplicationBuilder.GetStartupServices<TStartup>(startupConfiguration);

            var serviceProvider = ApplicationBuilder.UseStartup<TStartup>(startupServices, applicationName);
            return serviceProvider;
        }

        public static IServiceCollection GetStartupServices<TStartup>(IConfiguration startupConfiguration)
            where TStartup : class
        {
            var startupServices = new ServiceCollection()
                .AddConfiguration(startupConfiguration)
                .AddStartupConfiguration()
                .AddLogging(loggingBuilder =>
                {
                    var startupConfigurationInstance = loggingBuilder.Services.GetStartupConfiguration();
                    if (!startupConfigurationInstance.Configuration.IsEmpty())
                    {
                        var startupOptions = startupConfigurationInstance.Configuration.Get<StartupOptions>();

                        var startupLoggingConfigurationSection = startupConfigurationInstance.Configuration.GetSection(Constants.ConfigurationLoggingSectionKey);
                        if (!startupLoggingConfigurationSection.IsEmpty())
                        {
                            // Add startup logging configuration and providers.
                            loggingBuilder.AddConfiguration(startupLoggingConfigurationSection);
                        }

                        if (startupOptions.LogToConsole)
                        {
                            loggingBuilder.AddSimpleConsole();
                        }

                        if (startupOptions.LogToFile)
                        {
                            loggingBuilder.AddSimpleFile();
                        }
                    }
                    // Else, no startup logging.
                })
                .AddSingleton<TStartup>()
                ;

            return startupServices;
        }

        public static IServiceProvider UseStartup<TStartup>(IServiceCollection startupServices, string applicationName)
            where TStartup : class, IApplicationStartup
        {
            var startupServiceProvider = startupServices.BuildServiceProvider();

            var applicationStartup = startupServiceProvider.GetRequiredService<TStartup>();
            var logger = startupServiceProvider.GetLogger<TStartup>();

            logger.LogInformationEmphasisDouble($@"{applicationName}: Application name.");
            logger.LogAspNetCoreEnvironmentAndOverride();

            var serviceProvider = ApplicationBuilder.UseStartup(applicationStartup, logger);
            return serviceProvider;
        }

        public static IServiceProvider UseStartup(IApplicationStartup applicationStartup, ILogger logger)
        {
            logger.LogInformation($@"Using startup: {applicationStartup.GetType().FullName}");

            // Configuration.
            var configurationBuilder = new ConfigurationBuilder();

            applicationStartup.ConfigureConfiguration(configurationBuilder);

            var configuration = configurationBuilder.Build(logger);

            // Services.
            var services = new ServiceCollection();

            services.AddConfiguration(configuration, logger);

            applicationStartup.ConfigureServices(services);

            // Service provider.
            var serviceProvider = services.BuildServiceProvider(logger);

            applicationStartup.Configure(serviceProvider);

            // Tests.
            if (applicationStartup.RunTests)
            {
                applicationStartup.TestServices(serviceProvider);
            }

            return serviceProvider;
        }

        /// <summary>
        /// Creates a scope from a service provider, and returns the service provider from that scope.
        /// </summary>
        public static IServiceProvider UseStartupGetScopedServiceProvider<TStartup>(string applicationName)
            where TStartup : class, IApplicationStartup
        {
            var scope = ApplicationBuilder.UseStartupGetServiceScope<TStartup>(applicationName);

            var serviceProvider = scope.ServiceProvider;
            return serviceProvider;
        }

        /// <summary>
        /// Gets an <see cref="IServiceScope"/> from an un-scoped service provider.
        /// </summary>
        public static IServiceScope UseStartupGetServiceScope<TStartup>(string applicationName)
            where TStartup : class, IApplicationStartup
        {
            var serviceProvider = ApplicationBuilder.UseStartupGetUnscopedServiceProvider<TStartup>(applicationName);

            var scope = serviceProvider.CreateScope();
            return scope;
        }

        /// <summary>
        /// Gets a service provider and returns it directly.
        /// </summary>
        public static IServiceProvider UseStartupGetUnscopedServiceProvider<TStartup>(string applicationName)
            where TStartup : class, IApplicationStartup
        {
            var serviceProvider = ApplicationBuilder.UseStartup<TStartup>(applicationName);
            return serviceProvider;
        }

        public static void EnsureApplicationName(string applicationName)
        {
            if (String.IsNullOrEmpty(applicationName))
            {
                throw new Exception(ApplicationBuilder.ApplicationMustHaveNameMessage);
            }
        }

        #region Configuration - Configuration Builder

        /// <summary>
        /// Gets a configuration with functionality added by <see cref="ApplicationBuilder.AddDefaultConfiguration(IConfigurationBuilder, ILogger)"/>.
        /// </summary>
        public static IConfigurationBuilder GetDefaultConfigurationBuilder(ILogger logger)
        {
            var output = new ConfigurationBuilder()
                .LogConfigurationBuilderConfigurationStart(logger)
                .AddDefaultConfiguration(logger);
            ;

            return output;
        }

        /// <summary>
        /// Adds the appsettings.json file, and the ASP.NET Core environment-specific appsettings.{ASP.NET Core environment}.json file.
        /// </summary>
        public static IConfigurationBuilder AddDefaultConfiguration(this IConfigurationBuilder configurationBuilder, ILogger logger)
        {
            var output = configurationBuilder.AddDefaultConfiguration(false, logger);
            return output;
        }

        public static IConfigurationBuilder AddDefaultConfiguration(this IConfigurationBuilder configurationBuilder, bool appSettingsFileIsOptional, ILogger logger)
        {
            configurationBuilder
                .AddAppSettingsFile(appSettingsFileIsOptional, logger)
                .AddAspNetCoreEnvironmentSpecificAppSettingsFile(logger)
                ;

            return configurationBuilder;
        }

        #endregion

        #region Services - ConfigureServices

        /// <summary>
        /// Gets a service collection with services added by <see cref="ApplicationBuilder.AddDefaultServices(IServiceCollection, ILogger)"/>.
        /// </summary>
        public static IServiceCollection GetDefaultServices(ILogger logger)
        {
            var services = new ServiceCollection()
                .AddDefaultServices(logger)
                ;

            return services;
        }

        /// <summary>
        /// Adds logging.
        /// For logging, the configuration logging section is added, and the <see cref="Logging.SimpleConsole.SimpleConsoleLoggerProvider"/> provider is added.
        /// </summary>
        public static IServiceCollection AddDefaultServices(this IServiceCollection services, ILogger logger)
        {
            services
                .AddLogging(logger, (loggingBuilder, loggerInstance) =>
                {
                    loggingBuilder.AddLoggingConfigurationSection(loggerInstance);
                    loggingBuilder.AddSimpleConsole(loggerInstance); // Note! This works, just on a BS background thread so that you don't actually see the log output.
                })
                ;

            return services;
        }

        #endregion
    }
}
