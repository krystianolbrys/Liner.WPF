using System;
using System.Collections.Generic;
using Liner.API.Service.IoC;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.App.IoC
{
    public class IoCProviderFactory
    {
        public IServiceProvider Provide()
        {
            var services = new ServiceCollection();
            InstallServices(services);
            return services.BuildServiceProvider();
        }

        private void InstallServices(IServiceCollection services)
        {
            var installers = new List<IServiceInstaller>
            {
                new ApiServiceInstaller(),
                new AppInstaller()
            };

            installers.ForEach(
                (installer) => installer.Install(services));
        }
    }
}
