using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace R5T.NetStandard.Startup
{
    public abstract class StartupBase : IStartup
    {
        protected ILogger StartupLogger { get; }
        /// <summary>
        /// Tests are enabled by default (true).
        /// </summary>
        public virtual bool RunTests => true;


        public StartupBase(ILogger<StartupBase> startupLogger)
        {
            this.StartupLogger = startupLogger;

            startupLogger.LogInformationEmphasisDouble($@"{this.GetType().Name}: Application startup class.");
        }

        public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
        {
            this.StartupLogger.LogConfigureConfigurationStart();

            this.ConfigureConfigurationBody(configurationBuilder);

            this.StartupLogger.LogConfigureConfigurationEnd();
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureConfigurationBody(IConfigurationBuilder configurationBuilder)
        {
            // Do nothing.
        }

        public void ConfigureServices(IServiceCollection services)
        {
            this.StartupLogger.LogConfigureServicesStart();

            this.ConfigureServicesBody(services);

            this.StartupLogger.LogConfigureServicesEnd();
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void ConfigureServicesBody(IServiceCollection services)
        {
            // Do nothing.
        }

        /// <summary>
        /// Tests that logging works.
        /// </summary>
        public void TestServices(IServiceProvider serviceProvider)
        {
            this.StartupLogger.LogTestServicesStart();

            this.TestServicesBody(serviceProvider);

            this.StartupLogger.LogTestServicesEnd();
        }

        /// <summary>
        /// Base implementation does nothing.
        /// </summary>
        protected virtual void TestServicesBody(IServiceProvider serviceProvider)
        {
            // Do nothing.
        }
    }
}
