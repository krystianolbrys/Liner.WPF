using System.Reflection;
using Liner.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.Service.IoC
{
    public class LinerServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
