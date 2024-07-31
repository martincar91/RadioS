using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Presentation.Models;

public class GetDocumentRequestModel
{
    public string ProductCode { get; set; }
    public Guid TenantId { get; set; }
    public Guid DocumentId { get; set; }
}
