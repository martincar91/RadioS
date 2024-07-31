using MediatR;
using RadioS.Application.Entities;
namespace RadioS.Application.Models;

public class GetDocumentById
{
    public record Query(Guid tenantId, Guid documentId) : IRequest<Result>;

    public record Result(string financialDocument);
}   
