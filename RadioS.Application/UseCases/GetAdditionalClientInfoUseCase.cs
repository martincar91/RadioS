

using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;

namespace RadioS.Application.UseCases;

internal class GetAdditionalClientInfoUseCase : IRequestHandler<GetAdditionalClientInfo.Query, GetAdditionalClientInfo.Result>
{
    private readonly IClientRepository _clientRepository;

    public GetAdditionalClientInfoUseCase(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<GetAdditionalClientInfo.Result> Handle(GetAdditionalClientInfo.Query request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetAdditionalClientInfo(request.clientVAT);
        return new GetAdditionalClientInfo.Result(client);
    }
}
