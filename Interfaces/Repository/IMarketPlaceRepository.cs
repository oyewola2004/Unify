using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;
using Model.Enums;
using UNIFY.Interfaces.Repository;

namespace Interfaces.Repository
{
    public interface IMarketPlaceRepository : IRepository<MarketPlace>
    {
         Task<List<MarketPlace>> GetMarketPlaceByType(ServiceType ServiceType);
    }
}