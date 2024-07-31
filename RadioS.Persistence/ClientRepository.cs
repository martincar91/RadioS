using Newtonsoft.Json;
using RadioS.Application.Contracts;
using RadioS.Application.Entities;
using RadioS.Application.ViewModels;

namespace RadioS.Persistence.MockData;

public class ClientRepository : IClientRepository
{
    public Task<AdditionalClientInfo> GetAdditionalClientInfo(string VAT)
    {
        string filePath = @"C:\Users\Martin\Downloads\RadioS\RadioS\RadioS.Proxies\MockData\data.json";
        string json = File.ReadAllText(filePath);
        Root data = JsonConvert.DeserializeObject<Root>(json);

        var result = new AdditionalClientInfo();
        foreach (var tenant in data.Tenants)
        {
            if(tenant.Clients.Any(client => client.ClientVAT == VAT))
            {
                var client = tenant.Clients.Single(client => client.ClientVAT == VAT);
                result.RegistrationNumber = client.RegistrationNumber;
                result.CompanyType = client.CompanyType;
            }
        }
        return Task.FromResult(result);
    }

    public Task<Client> GetClient(Guid tenantId, Guid documentId)
    {
        Client client = new Client
        {
            ClientId = new Guid(),
            ClientName = "Test",
            ClientVAT = "aaaa",
            Documents = new List<Document>()
        };

        return Task.FromResult(client);
    }

    public Task<ClientInfo> GetClientIdAndVAT(Guid tenantId, Guid documentId)
    {
        string filePath = @"C:\Users\Martin\Downloads\RadioS\RadioS\RadioS.Proxies\MockData\data.json";
        string json = File.ReadAllText(filePath);
        Root data = JsonConvert.DeserializeObject<Root>(json);

        var tenant = data.Tenants.SingleOrDefault(tenant => tenant.TenantId == tenantId);
        var client = tenant.Clients.SingleOrDefault(client => client.Documents.Any(document => document.DocumentId == documentId));
        var result = new ClientInfo { CliendId = client.ClientId, ClientVAT = client.ClientVAT };
        return Task.FromResult(result);
    }
}
