

using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;

namespace RadioS.Application.UseCases;

public class GetClientIdAndVATUseCase : IRequestHandler<GetClientIdAndVAT.Query, GetClientIdAndVAT.Result>
{
    private readonly IClientRepository _clientRepository;

    public GetClientIdAndVATUseCase(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<GetClientIdAndVAT.Result> Handle(GetClientIdAndVAT.Query request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientIdAndVAT(request.tenantId, request.documentId);
        return new GetClientIdAndVAT.Result(client);
    }
}
