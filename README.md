[![](https://img.shields.io/nuget/v/soenneker.apollo.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.apollo.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.apollo.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.apollo.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.apollo.httpclients/)

# Soenneker.Apollo.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Apollo.HttpClients
```

## Quick start

```csharp
using Soenneker.Apollo.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddApolloOpenApiHttpClientAsSingleton();
```

Adds `ApolloOpenApiHttpClient` as a singleton service.

## What you get

- `IApolloOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `ApolloOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ApolloOpenApiHttpClientRegistrar.AddApolloOpenApiHttpClientAsSingleton(services)` | Adds `ApolloOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `ApolloOpenApiHttpClientRegistrar.AddApolloOpenApiHttpClientAsScoped(services)` | Adds `ApolloOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
