using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Application.UseCases;

public class IsWhitelistedClientUseCase : IRequestHandler<IsWhitelistedClient.Query, IsWhitelistedClient.Result>
{
    public readonly IWhiteListServiceStrategy _whiteListServiceStrategy;

    public IsWhitelistedClientUseCase(IWhiteListServiceStrategy whiteListServiceStrategy)
    {
        _whiteListServiceStrategy = whiteListServiceStrategy;
    }

    public async Task<IsWhitelistedClient.Result> Handle(IsWhitelistedClient.Query request, CancellationToken cancellationToken)
    {
        var clientWhiteListService = _whiteListServiceStrategy.GetWhiteListService(WhiteListServiceType.Client);

        if (!await clientWhiteListService.IsWhiteListed(request.clientId))
        {
            return new IsWhitelistedClient.Result(false);
        }
        return new IsWhitelistedClient.Result(true);
    }
}
