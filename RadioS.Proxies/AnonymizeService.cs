using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RadioS.Application.Contracts;
using RadioS.Application.Models;
using System.Security.Cryptography;
using System.Text;

namespace RadioS.Proxies;

public class AnonymizeService : IAnonymizeService
{
    private readonly AnonymizationSettings _settings;

    public AnonymizeService(IOptions<AnonymizationSettings> options)
    {
        _settings = options.Value;
    }

    public string AnonymizeData(string json, string productCode)
    {
        var anonymizationRule = _settings.AnonymizationConfigs[productCode];
        var hashFields = anonymizationRule.Hashed;
        var maskFields = anonymizationRule.Masked;

        var obj = JObject.Parse(json);
        AnonymizeObject(obj, hashFields, maskFields);
        return obj.ToString();
    }

    private void AnonymizeObject(JObject obj, List<string> hashFields, List<string> maskFields)
    {
        foreach (var property in obj.Properties())
        {
            if (property.Value.Type == JTokenType.Object)
            {
                AnonymizeObject((JObject) property.Value, hashFields, maskFields);
            }
            else if (property.Value.Type == JTokenType.Array)
            {
                foreach (var item in property.Value.Children<JObject>())
                {
                    AnonymizeObject(item, hashFields, maskFields);
                }
            }
            else
            {
                if (hashFields.Contains(property.Name))
                {
                    property.Value = Hash(property.Value.ToString());
                }
                else if (maskFields.Contains(property.Name))
                {
                    property.Value = "######";
                }
            }
        }
    }

    private string Hash(string value)
    {
        using (var sha256 = SHA256.Create()) 
        { 
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value)); 
            var stringBuilder = new StringBuilder(); 
            foreach (var b in bytes) 
            { 
                stringBuilder.Append(b.ToString("x2")); 
            } 
            return stringBuilder.ToString(); 
        } 
    }
}
