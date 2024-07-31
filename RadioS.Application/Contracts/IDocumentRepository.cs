using RadioS.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Application.Contracts;

public interface IDocumentRepository
{
    public string GetDocument(Guid tenantId, Guid documentId);
}
