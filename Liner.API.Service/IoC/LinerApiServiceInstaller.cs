using System.Reflection;
using Liner.API.Contracts;
using Liner.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.API.Service.IoC
{
    public class LinerApiServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<ILinerApiService, ApiService>();
        }
    }
}
