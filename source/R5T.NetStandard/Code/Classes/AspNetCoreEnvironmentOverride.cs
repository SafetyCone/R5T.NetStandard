using System;

using Microsoft.Extensions.Configuration;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard
{
    /// <summary>
    /// Allows overriding the ASP.NET Core environment variable with a value from a configuration file.
    /// If called in Program.Main() before the main web host configuration process is started, this will 
    /// </summary>
    public static class AspNetCoreEnvironmentOverride
    {
        public const string AspNetCoreEnvironmentOverrideConfigurationKey = @"AspNetCoreEnvironmentOverride";


        /// <summary>
        /// The initial ASP.NET Core environment.
        /// Specifically, the value of the ASP.NET Core environment at the time of first use of the override class.
        /// </summary>
        public static AspNetCoreEnvironmentValue OriginalAspNetCoreEnvironment { get; }

        public static bool IsOverriden
        {
            get
            {
                var aspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();

                var output = aspNetCoreEnvironment != AspNetCoreEnvironmentOverride.OriginalAspNetCoreEnvironment;
                return output;
            }
        }


        static AspNetCoreEnvironmentOverride()
        {
            AspNetCoreEnvironmentOverride.OriginalAspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();
        }

        /// <summary>
        /// Overrides the ASP.NET Core environment variable with a value from the default appsettings file.
        /// </summary>
        public static void Run(FilePath configurationJsonFilePath)
        {
            // Load the configuration file.
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(configurationJsonFilePath.Value)
                ;

            var configuration = configurationBuilder.Build();

            var aspNetCoreEnvironmentOverrideExists = configuration.Exists(AspNetCoreEnvironmentOverride.AspNetCoreEnvironmentOverrideConfigurationKey);
            if (aspNetCoreEnvironmentOverrideExists)
            {
                var aspNetCoreEnvironmentOverrideValue = configuration.GetAspNetCoreEnvironmentValueOrDefault();

                AspNetCoreEnvironment.Set(aspNetCoreEnvironmentOverrideValue);
            }
        }

        public static void Run()
        {
            AspNetCoreEnvironmentOverride.Run(AppSettings.GetDefaultFilePath());
        }
    }
}
