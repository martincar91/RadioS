
namespace RadioS.Application.Entities;

public class Client
{
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientVAT { get; set; }
    public string RegistrationNumber { get; set; }
    public string CompanyType { get; set; }
    public List<Document> Documents { get; set; }
}
