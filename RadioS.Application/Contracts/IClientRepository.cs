using RadioS.Application.ViewModels;

namespace RadioS.Application.Contracts;

public interface IClientRepository
{
    public Task<ClientInfo> GetClientIdAndVAT(Guid tenantId, Guid documentId);
    public Task<AdditionalClientInfo> GetAdditionalClientInfo(string VAT);
}
