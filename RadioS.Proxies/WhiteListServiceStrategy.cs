using RadioS.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioS.Proxies;

public class WhiteListServiceStrategy : IWhiteListServiceStrategy
{
    private readonly IEnumerable<IWhiteListService> _services;
    public WhiteListServiceStrategy(IEnumerable<IWhiteListService> services)
    {
        _services = services;
    }
    public IWhiteListService GetWhiteListService(WhiteListServiceType type)
    {
        return _services.Single(s => s.Type == type);
    }
}
