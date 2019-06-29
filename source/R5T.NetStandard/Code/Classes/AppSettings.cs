using System;

using R5T.NetStandard.IO;
using R5T.NetStandard.IO.Paths;
using R5T.NetStandard.IO.Paths.Extensions;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;
using PathUtilitiesExtra = R5T.NetStandard.IO.Paths.UtilitiesExtra;


namespace R5T.NetStandard
{
    public static class AppSettings
    {
        public static FileName DefaultJsonFileName { get; } = PathUtilities.GetFileName(Constants.AppSettingsBaseFileNameWithoutExtension, FileExtensions.Json);
        public static FileName DefaultDevelopmentJsonFileName { get; } = AppSettings.GetFileNameForEnvironment(AspNetCoreEnvironmentValue.Development);
        public static FileName DefaultStagingJsonFileName { get; } = AppSettings.GetFileNameForEnvironment(AspNetCoreEnvironmentValue.Staging);
        public static FileName DefaultProductionJsonFileName { get; } = AppSettings.GetFileNameForEnvironment(AspNetCoreEnvironmentValue.Production);


        public static FileName GetDefaultFileName()
        {
            return AppSettings.DefaultJsonFileName;
        }

        public static FilePath GetDefaultFilePath()
        {
            var defaultFileName = AppSettings.GetDefaultFileName();

            var defaultFilePath = PathUtilitiesExtra.RelativeToCurrentDirectory(defaultFileName);
            return defaultFilePath;
        }

        /// <summary>
        /// Returns the ASP.NET Core environment-specific default appsettings file name.
        /// </summary>
        public static FileName GetDefaultFileNameForEnvironment(AspNetCoreEnvironmentValue aspNetCoreEnvironment)
        {
            switch (aspNetCoreEnvironment)
            {
                case AspNetCoreEnvironmentValue.Development:
                    return AppSettings.DefaultDevelopmentJsonFileName;

                case AspNetCoreEnvironmentValue.Staging:
                    return AppSettings.DefaultStagingJsonFileName;

                case AspNetCoreEnvironmentValue.Production:
                    return AppSettings.DefaultProductionJsonFileName;

                case AspNetCoreEnvironmentValue.Default:
                case AspNetCoreEnvironmentValue.None:
                default:
                    return AppSettings.DefaultJsonFileName;
            }
        }

        public static FilePath GetDefaultFilePathForEnvironment(AspNetCoreEnvironmentValue aspNetCoreEnvironment)
        {
            var defaultFileName = AppSettings.GetDefaultFileNameForEnvironment(aspNetCoreEnvironment);

            var defaultFilePath = PathUtilitiesExtra.RelativeToCurrentDirectory(defaultFileName);
            return defaultFilePath;
        }

        /// <summary>
        /// Uses the current ASP.NET Core environment value to get an ASP.NET Core environement-specific appsettings file name.
        /// This method is limited to the values in the <see cref="AspNetCoreEnvironmentValue"/> enumeration.
        /// </summary>
        public static FileName GetFileNameForEnvironment()
        {
            var aspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();

            var output = AppSettings.GetDefaultFileNameForEnvironment(aspNetCoreEnvironment);
            return output;
        }

        public static FilePath GetFilePathForEnvironment()
        {
            var defaultFileName = AppSettings.GetFileNameForEnvironment();

            var defaultFilePath = PathUtilitiesExtra.RelativeToCurrentDirectory(defaultFileName);
            return defaultFilePath;
        }

        /// <summary>
        /// Uses the current ASP.NET Core environment environment variable value to get an ASP.NET Core environment-specific appsettings file name.
        /// This is useful when the ASP.NET Core environment is something other than the values supported by the <see cref="AspNetCoreEnvironmentValue"/> enumeration.
        /// </summary>
        public static FileName GetFileNameForEnvironmentVariable()
        {
            var aspNetCoreEnvironmentName = AspNetCoreEnvironment.GetVariableValue();

            var output = AppSettings.GetFileNameForEnvironment(aspNetCoreEnvironmentName);
            return output;
        }

        public static FileName GetFileNameForEnvironment(AspNetCoreEnvironmentValue aspNetCoreEnvironment)
        {
            var aspNetCoreEnvironmentName = aspNetCoreEnvironment.ToStringStandard();

            var output = AppSettings.GetJsonFileNameForEnvironment(Constants.AppSettingsBaseFileNameWithoutExtension, aspNetCoreEnvironmentName);
            return output;
        }

        public static FileName GetFileNameForEnvironment(string aspNetCoreEnvironmentName)
        {
            var output = AppSettings.GetJsonFileNameForEnvironment(Constants.AppSettingsBaseFileNameWithoutExtension, aspNetCoreEnvironmentName);
            return output;
        }


        #region General Utilities

        /// <summary>
        /// Returns an ASP.NET Core environment-specific JSON file-name for a base file-name and ASP.NET Core environment name.
        /// </summary>
        public static FileName GetJsonFileNameForEnvironment(FileNameWithoutExtension baseFileNameWithoutExtension, string aspNetCoreEnvironmentName)
        {
            var fileNameWithoutExtension = PathUtilities.Combine(baseFileNameWithoutExtension, aspNetCoreEnvironmentName.AsFileNameSegment()).AsFileNameWithoutExtension();

            var output = PathUtilities.GetFileName(fileNameWithoutExtension, FileExtensions.Json);
            return output;
        }

        public static FileName GetJsonFileNameForEnvironment(FileNameWithoutExtension baseFileNameWithoutExtension, AspNetCoreEnvironmentValue aspNetCoreEnvironment)
        {
            var aspNetCoreEnvironmentName = aspNetCoreEnvironment.ToStringStandard();

            var output = AppSettings.GetJsonFileNameForEnvironment(baseFileNameWithoutExtension, aspNetCoreEnvironmentName);
            return output;
        }

        #endregion
    }
}
