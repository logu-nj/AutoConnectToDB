using AutoConnectToDB.Persistence;
using AutoConnectToDB.Services;
using Microsoft.EntityFrameworkCore;

namespace AutoConnectToDB
{
    public static class Setup
    {
        public static IApplicationBuilder CORSPolicies(this IApplicationBuilder app)
        {
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            
            using var scope = app.ApplicationServices.CreateScope();

            scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
                .InitializeDatabasesAsync().GetAwaiter().GetResult();

            return app;
        }

        public static IServiceCollection InjectObject(this IServiceCollection services)
        {
            services.AddTransient<IClass1, Class1>();
            return services;
        }

        public static IServiceCollection AddDB(this IServiceCollection services,IConfiguration config)
        {
            string str = config.GetConnectionString("DBString");
            services.AddDbContext<ContextForDB>(options=>options.UseNpgsql(str));

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>()
              .AddTransient<ApplicationDbInitializer>();
            return services;
        }
    }
}
