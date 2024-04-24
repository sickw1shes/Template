using Template.Persistence.Common;
using Microsoft.Extensions.DependencyInjection;
using Template.Application.Services;

namespace Template.Application
{
    public static class ApplicationConfiguration
    {
        //DI
        public static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuditUserProvider, AuditUserProvider>();
        }
    }
}
