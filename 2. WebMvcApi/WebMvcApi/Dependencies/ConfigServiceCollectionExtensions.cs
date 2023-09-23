using Microsoft.EntityFrameworkCore;

using WebMvcApi.DBConnections;
using WebMvcApi.DBContexts;
using WebMvcApi.Models;
using WebMvcApi.Repositories;
using WebMvcApi.CustomFramework;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ConfigServiceCollectionExtensions {
        public static IServiceCollection AddTodoItemsInmemoryContextWithRepository(this IServiceCollection services) {
            services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddTransient<IRepository<TodoItem>, TodoContextRepository>();
            return services;
        }
        public static IServiceCollection AddUsersPostgresContextWithRepository(this IServiceCollection services, string postgreConnectionString) {
            services.AddDbContext<UserContext>(opt => opt.UseNpgsql(postgreConnectionString));
            services.AddTransient<IRepository<User>, UserContextRepository>();
            return services;
        }
        public static IServiceCollection AddUsersPostgresConnectionWithVisitorAndRepository(this IServiceCollection services, string postgreConnectionString) {
            services.AddTransient(opt => new PostgreConnection(postgreConnectionString));
            services.AddTransient(opt => new CustomSQLParseVisitor("Users"));
            services.AddTransient<UserVisitorRepository>();
            return services;
        }
    }
}
