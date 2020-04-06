using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InternalComplexTask.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<TContext>().Database.Migrate();
        }
    }
}
