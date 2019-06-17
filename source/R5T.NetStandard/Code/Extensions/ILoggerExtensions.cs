using System;

using Microsoft.Extensions.Logging;


namespace R5T.NetStandard
{
    public static class ILoggerExtensions
    {
        public static void LogAspNetCoreEnvironmentAndOverride(this ILogger log)
        {
            var aspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();

            log.LogInformation($@"--- {aspNetCoreEnvironment.ToStringStandard()}: ASP.NET Core Environment ---");

            if (AspNetCoreEnvironmentOverride.IsOverriden)
            {
                var originalEnvironment = AspNetCoreEnvironmentOverride.OriginalAspNetCoreEnvironment.ToStringStandard();
                log.LogInformation($@"--- OVERRIDE {originalEnvironment}: Original ASP.NET Core Environment '{originalEnvironment}'. ---");
            }
        }

        public static void LogBuiltConfiguration(this ILogger log)
        {
            log.LogInformationEmphasis(@"Built configuration.");
        }
    }
}
