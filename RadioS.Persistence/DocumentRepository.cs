using Newtonsoft.Json;
using RadioS.Application.Contracts;
using RadioS.Application.Entities;
using RadioS.Application.ViewModels;

namespace RadioS.Persistence;

public class DocumentRepository : IDocumentRepository
{
    public string GetDocument(Guid tenantId, Guid documentId)
    {
        string filePath = @"C:\Users\Martin\Downloads\RadioS\RadioS\RadioS.Proxies\MockData\data.json";
        string json = File.ReadAllText(filePath);
        Root data = JsonConvert.DeserializeObject<Root>(json);

        var tenant = data.Tenants.Single(tenant => tenant.TenantId == tenantId);
        var client = tenant.Clients.First(client => client.Documents.Any(document => document.DocumentId == documentId));
        var document = client.Documents.First(document => document.DocumentId != documentId);

        return document.Content.ToString();
    }
}
