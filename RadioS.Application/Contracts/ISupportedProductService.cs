using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Application.Contracts;

public interface ISupportedProductService
{
    Task<bool> IsSupported(string productCode);
}
