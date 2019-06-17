using System;
using System.IO;


namespace R5T.NetStandard
{
    /// <summary>
    /// The ASP.NET Core environment functionality is too useful to be used only for ASP.NET Core web projects. Even console applications can benefit, thus the functionality is put into the .NET Standard library.
    /// </summary>
    public static class AspNetCoreEnvironment
    {
        public const string EnvironmentVariableName = @"ASPNETCORE_ENVIRONMENT";
        public const string DevelopmentEnvironmentName = @"Development";
        public const string StagingEnvironmentName = @"Staging";
        public const string ProductionEnvironmentName = @"Production";
        /// <summary>
        /// Indicates that no action should be taken to change the ASP.NET Core environment variable. Whatever was going to happen should happen.
        /// </summary>
        public const string Default = @"Default";
        /// <summary>
        /// Setting an environment variable to have a null or empty value deletes it.
        /// </summary>
        public const string None = null;



        public static string VariableValue
        {
            get
            {
                var output = AspNetCoreEnvironment.GetVariableValue();
                return output;
            }
        }
        public static AspNetCoreEnvironmentValue Value
        {
            get
            {
                var output = AspNetCoreEnvironment.GetValue();
                return output;
            }
        }
        public static bool IsDevelopment
        {
            get
            {
                var output = AspNetCoreEnvironment.Is(AspNetCoreEnvironment.DevelopmentEnvironmentName);
                return output;
            }
        }
        public static bool IsStaging
        {
            get
            {
                var output = AspNetCoreEnvironment.Is(AspNetCoreEnvironment.StagingEnvironmentName);
                return output;
            }
        }
        public static bool IsProduction
        {
            get
            {
                var output = AspNetCoreEnvironment.Is(AspNetCoreEnvironment.ProductionEnvironmentName);
                return output;
            }
        }


        public static bool Is(string environmentName)
        {
            var aspNetCoreEnvironmentVariable = AspNetCoreEnvironment.VariableValue;

            var output = aspNetCoreEnvironmentVariable == environmentName;
            return output;
        }

        public static string GetVariableValue()
        {
            var output = Environment.GetEnvironmentVariable(AspNetCoreEnvironment.EnvironmentVariableName);
            return output;
        }

        public static AspNetCoreEnvironmentValue GetValue()
        {
            var variableValue = AspNetCoreEnvironment.GetVariableValue();
            switch (variableValue)
            {
                case AspNetCoreEnvironment.DevelopmentEnvironmentName:
                    return AspNetCoreEnvironmentValue.Development;

                case AspNetCoreEnvironment.StagingEnvironmentName:
                    return AspNetCoreEnvironmentValue.Staging;

                case AspNetCoreEnvironment.ProductionEnvironmentName:
                    return AspNetCoreEnvironmentValue.Production;

                case AspNetCoreEnvironment.None:
                    return AspNetCoreEnvironmentValue.None;

                default:
                    throw new Exception($@"Unknown ASP.NET Core environment name: {variableValue}");
            }
        }

        public static void Set(string environmentName)
        {
            Environment.SetEnvironmentVariable(AspNetCoreEnvironment.EnvironmentVariableName, environmentName);
        }

        public static void SetDevelopment()
        {
            AspNetCoreEnvironment.Set(AspNetCoreEnvironment.DevelopmentEnvironmentName);
        }

        public static void SetStaging()
        {
            AspNetCoreEnvironment.Set(AspNetCoreEnvironment.StagingEnvironmentName);
        }

        public static void SetProduction()
        {
            AspNetCoreEnvironment.Set(AspNetCoreEnvironment.ProductionEnvironmentName);
        }

        public static void SetNone()
        {
            AspNetCoreEnvironment.Set(AspNetCoreEnvironment.None);
        }

        public static void Set(AspNetCoreEnvironmentValue value)
        {
            switch (value)
            {
                case AspNetCoreEnvironmentValue.Development:
                    AspNetCoreEnvironment.SetDevelopment();
                    break;

                case AspNetCoreEnvironmentValue.Staging:
                    AspNetCoreEnvironment.SetStaging();
                    break;

                case AspNetCoreEnvironmentValue.Production:
                    AspNetCoreEnvironment.SetProduction();
                    break;

                case AspNetCoreEnvironmentValue.None:
                    AspNetCoreEnvironment.SetNone();
                    break;

                case AspNetCoreEnvironmentValue.Default:
                default:
                    // Do nothing.
                    break;
            }
        }

        public static string GetEnvironmentSpecificRootDirectoryPath(string rootDirectoryPath, AspNetCoreEnvironmentValue aspNetCoreEnvironment)
        {
            var aspNetCoreEnvironmentString = aspNetCoreEnvironment.ToStringStandard();

            var output = Path.Combine(rootDirectoryPath, aspNetCoreEnvironmentString);
            return output;
        }

        public static string GetEnvironmentSpecificRootDirectoryPath(string rootDirectoryPath)
        {
            var aspNetCoreEnvironment = AspNetCoreEnvironment.GetValue();

            var output = AspNetCoreEnvironment.GetEnvironmentSpecificRootDirectoryPath(rootDirectoryPath, aspNetCoreEnvironment);
            return output;
        }
    }
}
