using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Persistence
{
    public static class PersistenceConfiguration
    {
        //прокинуть подключение к базам данных
        public static void AddServices(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<PostgreDbContext>(options => options.UseNpgsql(connectionString));
        }
    }
}
