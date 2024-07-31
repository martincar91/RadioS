using RadioS.Application.Contracts;

namespace RadioS.Proxies;

public class ClientWhiteListService : IWhiteListService
{
    private readonly Guid[] _whitelistedClients = new Guid[] {
        Guid.Parse("54b56bd2-1aea-4b66-b489-1440036bee8c"),
        Guid.Parse("be0d29c8-f7c1-4d14-bfc1-d85c0d1cddbd"),
        Guid.Parse("614cf63d-579e-4176-b3e1-eb5c78b90ccf")
    };
    public WhiteListServiceType Type => WhiteListServiceType.Client;

    public Task<bool> IsWhiteListed(Guid clientId)
    {
        //todo implement
        //var result = _whitelistedClients.Contains(clientId);
        return Task.FromResult(true);
    }
}
