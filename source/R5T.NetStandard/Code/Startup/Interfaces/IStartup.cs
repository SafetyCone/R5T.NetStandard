using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace R5T.NetStandard
{
    public interface IStartup
    {
        bool RunTests { get; }


        void ConfigureConfiguration(IConfigurationBuilder configurationBuilder);
        void ConfigureServices(IServiceCollection services);
        //void Configure(IServiceProvider serviceProvider); // No Configure() since web-applications will need Configure(IApplicationBuilder).
        void TestServices(IServiceProvider serviceProvider);
    }
}
