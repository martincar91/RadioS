
namespace RadioS.Application.Entities;

public class Document
{
    public Guid DocumentId { get; set; }
    public string ProductCode { get; set; }
    public object Content { get; set; } // Content can vary depending on ProductCode
}
