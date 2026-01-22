using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Films.Domain.Interfaces.Repositories;
using Films.Infrastructure.Data;
using Films.Infrastructure.Repositories;

namespace Films.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<FilmsDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(FilmsDbContext).Assembly.FullName)));

        // Register repositories
        services.AddScoped<IActorRepository, ActorRepository>();
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<IDirectedByRepository, DirectedByRepository>();
        services.AddScoped<ISexRepository, SexRepository>();
        services.AddScoped<ITypeUserRepository, TypeUserRepository>();

        return services;
    }
}
