using Microsoft.Extensions.DependencyInjection;
using Films.Application.Services;
using Films.Domain.Interfaces.Services;

namespace Films.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Register services
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<IDirectedByService, DirectedByService>();

        return services;
    }
}
