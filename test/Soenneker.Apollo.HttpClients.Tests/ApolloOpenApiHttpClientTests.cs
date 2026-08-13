using Soenneker.Apollo.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Apollo.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ApolloOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IApolloOpenApiHttpClient _httpclient;

    public ApolloOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IApolloOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
