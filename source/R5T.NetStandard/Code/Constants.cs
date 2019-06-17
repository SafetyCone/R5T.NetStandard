using System;

using R5T.NetStandard.IO.Paths;


namespace R5T.NetStandard
{
    public static class Constants
    {
        public static FileNameWithoutExtension AppSettingsBaseFileNameWithoutExtension { get; }  = new FileNameWithoutExtension(@"appsettings");
    }
}
