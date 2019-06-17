using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace R5T.NetStandard
{
    public static class IServiceProviderExtensions
    {
        public static IConfiguration GetConfiguration(this IServiceProvider serviceProvider)
        {
            var output = serviceProvider.GetRequiredService<IConfiguration>();
            return output;
        }

        /// <summary>
        /// Directly gets the <typeparamref name="TOptions"/> value from the <see cref="IOptions{TOptions}"/> service.
        /// </summary>
        public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class, new()
        {
            var optionsOptions = serviceProvider.GetRequiredService<IOptions<TOptions>>();
            var options = optionsOptions.Value;
            return options;
        }

        #region Logging

        public static ILogger<T> GetLogger<T>(this IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<T>>();
            return logger;
        }

        public static ILogger<T> GetLogger<T>(this IServiceProvider serviceProvider, string functionName)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<T>>();

            logger.LogFunctionName(functionName);

            return logger;
        }

        public static ILogger GetLogger(this IServiceProvider serviceProvider, string categoryName)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger(categoryName);
            return logger;
        }

        public static ILogger GetLogger(this IServiceProvider serviceProvider, Type type)
        {
            var categoryName = type.Name;

            var logger = serviceProvider.GetLogger(categoryName);
            return logger;
        }

        /// <summary>
        /// Tests that logging is on by getting a log and writing to it.
        /// </summary>
        /// <typeparam name="TSetup">The Setup class type to that all setup logging has the same category name.</typeparam>
        public static IServiceProvider TestLoggingIsOn<TSetup>(this IServiceProvider serviceProvider)
        {
            serviceProvider.TestLoggingIsOn(typeof(TSetup));

            return serviceProvider;
        }

        public static IServiceProvider TestLoggingIsOn(this IServiceProvider serviceProvider, Type categoryType)
        {
            serviceProvider.TestLoggingIsOn(categoryType.Name);

            return serviceProvider;
        }

        public static IServiceProvider TestLoggingIsOn(this IServiceProvider serviceProvider, string categoryName)
        {
            var logger = serviceProvider.GetLogger(categoryName);

            logger.LogInformation(@"LOGGING - Verified service is on!");

            return serviceProvider;
        }

        public static void LogVerifiedServicesOn<TSetup>(this IServiceProvider serviceProvider)
        {
            var log = serviceProvider.GetLogger<TSetup>();

            log.LogVerifiedServicesOn();
        }

        #endregion
    }
}
