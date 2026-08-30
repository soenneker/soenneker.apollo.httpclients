using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
namespace Soenneker.Apollo.HttpClients.Abstract;
/// <summary>
/// Provides the cached, authenticated <see cref="HttpClient"/> used by the Apollo OpenAPI client.
/// </summary>
public interface IApolloOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured HTTP client used by the Apollo OpenAPI HTTP client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
