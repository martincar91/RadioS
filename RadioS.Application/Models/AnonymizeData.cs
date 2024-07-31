using MediatR;
using RadioS.Application.ViewModels;

namespace RadioS.Application.Models;

public class AnonymizeData
{
    public record Query(string financialDocument, string productCode) : IRequest<Result>;
    public record Result(string financialDocument);
}

public class AnonymizationSettings
{
    public Dictionary<string, AnonymizationConfig> AnonymizationConfigs { get; set; }
}

public class AnonymizationConfig
{
    public List<string> Hashed { get; set; }
    public List<string> Masked { get; set; }
    //public Dictionary<string, FieldTreatment> FieldTreatments { get; set; }
}

