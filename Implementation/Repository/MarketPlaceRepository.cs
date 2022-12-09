using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Enums;
using UNIFY.Context;
using UNIFY.Implementation.Repository;

namespace Implementation
{
    public class MarketPlaceRepository : BaseRepository<MarketPlace>, IMarketPlaceRepository
    {
         public MarketPlaceRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<MarketPlace>> GetMarketPlaceByType(ServiceType ServiceType)
        {
             var marketPlace =  await _context.MarketPlaces
            .Where( L => L.ServiceType== ServiceType).ToListAsync();
            return marketPlace;
        }
    }
}