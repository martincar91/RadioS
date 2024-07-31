
using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;

namespace RadioS.Application.UseCases;

public class AnonymizeDataUseCase : IRequestHandler<AnonymizeData.Query, AnonymizeData.Result>
{
    private readonly IAnonymizeService _anonymizeService;

    public AnonymizeDataUseCase(IAnonymizeService anonymizeService)
    {
        _anonymizeService = anonymizeService;
    }

    public async Task<AnonymizeData.Result> Handle(AnonymizeData.Query request, CancellationToken cancellationToken)
    {
        var anonymizedData = _anonymizeService.AnonymizeData(request.financialDocument, request.productCode);
        return new AnonymizeData.Result(anonymizedData);
    }
}
