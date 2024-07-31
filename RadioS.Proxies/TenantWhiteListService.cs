using RadioS.Application.Contracts;

namespace RadioS.Proxies;

public class TenantWhiteListService : IWhiteListService
{
    private readonly Guid[] _whitelistedTenants = new Guid[] {
        Guid.Parse("dca6e1c5-1b3b-4e5c-9d39-93b0f5a9e060") };
    public WhiteListServiceType Type => WhiteListServiceType.Tenant;

    public Task<bool> IsWhiteListed(Guid tenantId)
    {
        var result = _whitelistedTenants.Contains(tenantId);
        return Task.FromResult(result);
    }
}
