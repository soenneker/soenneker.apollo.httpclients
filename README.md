[![](https://img.shields.io/nuget/v/soenneker.apollo.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.apollo.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.apollo.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.apollo.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.httpclients/)

# Soenneker.Apollo.HttpClients

A cached, authenticated `HttpClient` provider for Apollo's REST API.

This is the HTTP transport layer used by `Soenneker.Apollo.OpenApiClientUtil`. Most applications that want the generated, typed Apollo API should register that higher-level package instead. Use this package directly when you need the configured `HttpClient` itself.

## Installation

```bash
dotnet add package Soenneker.Apollo.HttpClients
```

## Configuration

Add the Apollo API key to configuration:

```json
{
  "Apollo": {
    "ApiKey": "your-api-key"
  }
}
```

The available keys are:

| Key | Required | Default | Purpose |
| --- | --- | --- | --- |
| `Apollo:ApiKey` | Yes | — | Value used to authenticate requests. |
| `Apollo:ClientBaseUrl` | No | `https://api.apollo.io/api/v1` | Overrides the Apollo API base address. |
| `Apollo:AuthHeaderName` | No | `x-api-key` | Changes the authentication header name. |
| `Apollo:AuthHeaderValueTemplate` | No | `{token}` | Formats the header value; `{token}` is replaced with the configured API key. |

For example, a bearer-style gateway can be configured with an `Authorization` header and a `Bearer {token}` template.

Keep API keys in user secrets, environment variables, or a secret store rather than committing them to `appsettings.json`.

## Registration and usage

```csharp
using Soenneker.Apollo.HttpClients.Registrars;

builder.Services.AddApolloOpenApiHttpClientAsSingleton();
```

Inject the abstraction and retrieve the cached client:

```csharp
using Soenneker.Apollo.HttpClients.Abstract;

public sealed class ApolloTransportProbe
{
    private readonly IApolloOpenApiHttpClient _clientProvider;

    public ApolloTransportProbe(IApolloOpenApiHttpClient clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async ValueTask<Uri?> GetBaseAddress(
        CancellationToken cancellationToken)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return client.BaseAddress;
    }
}
```

`Get` creates the client on first use and returns the same cached instance afterward. The client has its `BaseAddress` and authentication header configured from `IConfiguration`.

Scoped registration is available, but the underlying HTTP client cache remains a singleton:

```csharp
builder.Services.AddApolloOpenApiHttpClientAsScoped();
```

## Lifetime behavior

- Reuse the returned client; do not dispose it after individual requests.
- Configuration is evaluated when the cached client is created. Changing configuration does not mutate an already-created client.
- Let the dependency-injection container dispose `IApolloOpenApiHttpClient`. Disposal removes its entry from the shared HTTP client cache.
- The registration methods use `TryAdd` for the provider, so an application-supplied implementation is preserved.

For typed endpoint methods and generated response models, use [`Soenneker.Apollo.OpenApiClientUtil`](https://www.nuget.org/packages/Soenneker.Apollo.OpenApiClientUtil).

## API

| Method | Purpose |
| --- | --- |
| `IApolloOpenApiHttpClient.Get(CancellationToken)` | Returns the cached, configured Apollo `HttpClient`. |
| `AddApolloOpenApiHttpClientAsSingleton()` | Registers the provider application-wide. |
| `AddApolloOpenApiHttpClientAsScoped()` | Registers a scoped provider backed by the shared client cache. |
