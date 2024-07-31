

namespace RadioS.Application.Contracts;

public interface IAnonymizeService
{
    public string AnonymizeData(string json, string productCode);
}
