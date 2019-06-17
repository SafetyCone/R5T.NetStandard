using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Startup
{
    public abstract class ApplicationStartupBase : StartupBase, IApplicationStartup
    {
        /// <summary>
        /// Specifies whether the appsettings file is optional for configuration.
        /// </summary>
        public virtual bool AppSettingsFileIsOptional => false;


        public ApplicationStartupBase(ILogger<ApplicationStartupBase> startupLogger)
            : base(startupLogger)
        {
        }

        /// <summary>
        /// Adds the default configuration via <see cref="ApplicationBuilder.AddDefaultConfiguration(IConfigurationBuilder, ILogger)"/>.
        /// </summary>
        protected override void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddDefaultConfiguration(this.AppSettingsFileIsOptional, this.StartupLogger)
                ;
        }

        /// <summary>
        /// Adds the default services <see cref="ApplicationBuilder.AddDefaultServices(IServiceCollection, ILogger)"/>.
        /// </summary>
        protected override void ConfigureServicesBody(IServiceCollection services)
        {
            services
                .AddDefaultServices(this.StartupLogger)
                ;
        }

        public void Configure(IServiceProvider serviceProvider)
        {
            this.StartupLogger.LogConfigureStart();

            this.ConfigureBody(serviceProvider);

            this.StartupLogger.LogConfigureEnd();
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureBody(IServiceProvider serviceProvider)
        {
            // Do nothing.
        }

        /// <summary>
        /// Tests that logging is on.
        /// </summary>
        protected override void TestServicesBody(IServiceProvider serviceProvider)
        {
            serviceProvider
                .TestLoggingIsOn(this.GetType())
                ;
        }
    }
}
