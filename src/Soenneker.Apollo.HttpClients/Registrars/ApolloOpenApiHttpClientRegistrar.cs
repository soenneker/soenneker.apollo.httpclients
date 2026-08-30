using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Apollo.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Apollo.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class ApolloOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IApolloOpenApiHttpClient"/> as a singleton service backed by the singleton HTTP-client cache.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddApolloOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IApolloOpenApiHttpClient, ApolloOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IApolloOpenApiHttpClient"/> as a scoped service backed by the singleton HTTP-client cache.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddApolloOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IApolloOpenApiHttpClient, ApolloOpenApiHttpClient>();

        return services;
    }
}
