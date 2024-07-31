using MediatR;

namespace RadioS.Application.Models;

public class IsWhitelistedClient
{
    public record Query(Guid tenantId, Guid clientId): IRequest<Result>;
    public record Result(bool isWhitelisted);
}
