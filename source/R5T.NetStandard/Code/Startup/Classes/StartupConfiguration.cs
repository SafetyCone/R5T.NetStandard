using System;

using Microsoft.Extensions.Configuration;


namespace R5T.NetStandard.Startup
{
    public class StartupConfiguration
    {
        public IConfiguration Configuration { get; }


        public StartupConfiguration(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
    }
}
