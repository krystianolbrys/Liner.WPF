using Liner.API.Contracts;
using Liner.API.Service;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.App.IoC
{
    public class AppInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddTransient<ILinerApiService, ApiService>();
        }
    }
}
