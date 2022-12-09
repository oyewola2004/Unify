using System.Threading.Tasks;
using Dtos;
using Model.Enums;
using UNIFY.Dtos;

namespace Interfaces.Services
{
    public interface IMarketPlaceService
    {
        Task<MarketPlaceResponseModel> CreateMarketPlace(CreateMarketPlaceRequestModel model);
        Task<BaseResponse> UpdateMarketPlace(UpdateMarketPlaceRequestModel model, string id);
        public Task<bool> Delete(string Id);
        Task<MarketPlacesResponseModel> GetAllMarketPlace();
        Task<MarketPlaceResponseModel> GetMarketById(string id);
         Task<MarketPlacesResponseModel> GetMarketPlaceByType(int ServiceType);
    }
}