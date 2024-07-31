using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;

namespace RadioS.Application.UseCases;

public class IsProductSupportedUseCase : IRequestHandler<IsProductSupported.Query, IsProductSupported.Result>
{
    private readonly ISupportedProductService _supportedProductService;

    public IsProductSupportedUseCase(ISupportedProductService supportedProductService)
    {
        _supportedProductService = supportedProductService;
    }

    public async Task<IsProductSupported.Result> Handle(IsProductSupported.Query request, CancellationToken cancellationToken)
    {
        var result = await _supportedProductService.IsSupported(request.productCode);
        return new IsProductSupported.Result(result);
    }
}
