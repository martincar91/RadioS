using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Application.Contracts;

public interface IWhiteListService
{
    WhiteListServiceType Type { get; }
    Task<bool> IsWhiteListed(Guid id);
}
