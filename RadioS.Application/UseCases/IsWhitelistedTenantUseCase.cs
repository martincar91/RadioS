using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Application.UseCases;

internal class IsWhitelistedTenantUseCase : IRequestHandler<IsWhitelistedTenant.Query, IsWhitelistedTenant.Result>
{
    public readonly IWhiteListServiceStrategy _whiteListServiceStrategy;

    public IsWhitelistedTenantUseCase(IWhiteListServiceStrategy whiteListServiceStrategy)
    {
        _whiteListServiceStrategy = whiteListServiceStrategy;
    }   
    
    public async Task<IsWhitelistedTenant.Result> Handle(IsWhitelistedTenant.Query request, CancellationToken cancellationToken)
    {
        var tenantWhiteListService = _whiteListServiceStrategy.GetWhiteListService(WhiteListServiceType.Tenant);

        if (!await tenantWhiteListService.IsWhiteListed(request.tenantId))
        {
            return new IsWhitelistedTenant.Result(false);
        }
        return new IsWhitelistedTenant.Result(true); 
    }
}
