using MediatR;

namespace RadioS.Application.Models;

public class IsWhitelistedTenant
{
    public record Query(Guid tenantId): IRequest<Result>;
    public record Result(bool isWhitelisted);
}
