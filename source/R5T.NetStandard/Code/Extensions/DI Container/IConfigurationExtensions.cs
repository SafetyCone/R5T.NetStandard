using System;

using Microsoft.Extensions.Configuration;


namespace R5T.NetStandard
{
    public static class IConfigurationExtensions
    {
        public static AspNetCoreEnvironmentValue GetAspNetCoreEnvironmentValueOrDefault(this IConfiguration configuration)
        {
            var aspNetCoreEnvironmentOverride = configuration[AspNetCoreEnvironmentOverride.AspNetCoreEnvironmentOverrideConfigurationKey];
            if (String.IsNullOrEmpty(aspNetCoreEnvironmentOverride))
            {
                return AspNetCoreEnvironmentValue.Default;
            }
            else
            {
                // Enusre that the input string is one of the allowed values.
                var output = EnumHelper.Parse<AspNetCoreEnvironmentValue>(aspNetCoreEnvironmentOverride);
                return output;
            }
        }
    }
}
