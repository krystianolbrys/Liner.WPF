using System.Reflection;
using Liner.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.API.Service.IoC
{
    public class ApiServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
