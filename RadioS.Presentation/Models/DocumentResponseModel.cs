using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Presentation.Models;

public  class DocumentResponseModel
{
    public string Data { get; set; }

}

public class CompanyInfoModel
{ 
    public string RegistrationNumber { get; set; }
    public string CompanyType { get; set; }
}

public class EnrichedDocumentModel: DocumentResponseModel
{
    public CompanyInfoModel Company  { get; set; }
}