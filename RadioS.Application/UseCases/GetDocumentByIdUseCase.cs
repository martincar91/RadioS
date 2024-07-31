using MediatR;
using RadioS.Application.Contracts;
using RadioS.Application.Models;

namespace RadioS.Application.UseCases;

public class GetDocumentByIdUseCase : IRequestHandler<GetDocumentById.Query, GetDocumentById.Result>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IWhiteListServiceStrategy _whiteListServiceStrategy;

    public GetDocumentByIdUseCase(
        IDocumentRepository documentRepository, 
        IWhiteListServiceStrategy whiteListServiceStrategy)
    {
        _documentRepository = documentRepository;
        _whiteListServiceStrategy = whiteListServiceStrategy;
    }

    async Task<GetDocumentById.Result> IRequestHandler<GetDocumentById.Query, GetDocumentById.Result>.Handle(GetDocumentById.Query request, CancellationToken cancellationToken)
    {
 
        var document = _documentRepository.GetDocument(request.tenantId, request.documentId);

        return new GetDocumentById.Result(document);
    }
}
