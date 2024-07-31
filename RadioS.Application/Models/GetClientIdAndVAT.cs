using MediatR;
using RadioS.Application.ViewModels;

namespace RadioS.Application.Models;

public class GetClientIdAndVAT
{
    public record Query(Guid tenantId, Guid documentId): IRequest<Result>;
    public record Result(ClientInfo client);
}
