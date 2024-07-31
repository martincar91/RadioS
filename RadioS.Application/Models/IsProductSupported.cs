using MediatR;

namespace RadioS.Application.Models;

public class IsProductSupported
{
    public record Query(string productCode): IRequest<Result>;
    public record Result(bool isSupported);
}
