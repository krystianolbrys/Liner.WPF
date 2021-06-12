using Microsoft.Extensions.DependencyInjection;

namespace Liner.Infrastructure
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection services);
    }
}
