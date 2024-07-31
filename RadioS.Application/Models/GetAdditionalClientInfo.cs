using MediatR;
using RadioS.Application.ViewModels;

namespace RadioS.Application.Models;

public class GetAdditionalClientInfo
{
    public record Query(string clientVAT): IRequest<Result>;
    public record Result(AdditionalClientInfo client);
}
