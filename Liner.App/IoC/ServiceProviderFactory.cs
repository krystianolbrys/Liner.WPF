using System;
using System.Collections.Generic;
using Liner.API.Service.IoC;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.App.IoC
{
    public class ServiceProviderFactory
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
                new ApiServiceInstaller(), // should be installed on API layer if it will be REST endpoint - api layer responsibility - used here only for simplicity
                new AppInstaller() // here should be installer for Refit Api caller using contract from Liner.API.Contracts and HttpClient provided by IHttpClientFactory for httpClien reusing 
            };

            installers.ForEach(
                (installer) => installer.Install(services));
        }
    }
}
