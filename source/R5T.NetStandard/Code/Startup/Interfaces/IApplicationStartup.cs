using System;


namespace R5T.NetStandard
{
    public interface IApplicationStartup : IStartup
    {
        void Configure(IServiceProvider serviceProvider);
    }
}
