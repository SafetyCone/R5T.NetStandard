using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.NetStandard.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static IServiceCollection AddStartupConfiguration(this IServiceCollection services)
        {
            var configuration = services.GetConfiguration();

            var startupConfigurationSection = configuration.GetSection(Constants.StartupConfigurationSectionKey);

            var startupConfiguration = new StartupConfiguration(startupConfigurationSection);

            services.AddSingleton<StartupConfiguration>(startupConfiguration);

            return services;
        }

        public static StartupConfiguration GetStartupConfiguration(this IServiceCollection services)
        {
            var startupConfiguration = services.GetIntermediateRequiredService<StartupConfiguration>();

            return startupConfiguration;
        }
    }
}
