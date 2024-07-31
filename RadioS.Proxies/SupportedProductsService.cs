using Newtonsoft.Json;
using RadioS.Application.Contracts;
using RadioS.Application.Entities;

namespace RadioS.Proxies;
public class SupportedProductsService: ISupportedProductService
{

    public Task<bool> IsSupported(string productCode)
    {
        string filePath = @"C:\Users\Martin\Downloads\RadioS\RadioS\RadioS.Proxies\MockData\data.json";
        string json = File.ReadAllText(filePath);
        Root data = JsonConvert.DeserializeObject<Root>(json);
        var isSupported = data?.Tenants.Any(tenant => tenant.Clients.Any(client => client.Documents.Any(document => document.ProductCode.Equals(productCode, StringComparison.OrdinalIgnoreCase)))) ?? false;
        return Task.FromResult(isSupported);
    }
}
