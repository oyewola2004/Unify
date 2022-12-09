using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dtos;
using Interfaces.Repository;
using Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Model.Entities;
using Model.Enums;
using UNIFY.Dtos;

namespace Implementation.Services
{
    public class MarketPlaceService : IMarketPlaceService
    {

         private readonly IMarketPlaceRepository _marketPlaceRepository;
        private readonly IWebHostEnvironment _webHost;
         public MarketPlaceService(IWebHostEnvironment webHost, IMarketPlaceRepository marketPlaceRepository)
        {
            _marketPlaceRepository = marketPlaceRepository;
            _webHost = webHost;
        }
        public async Task<MarketPlaceResponseModel> CreateMarketPlace(CreateMarketPlaceRequestModel model)
        {
            var marketPlace = new MarketPlace
            {
                Image = model.Image,
                ServiceType = model.ServiceType,
                CompanyName = model.CompanyName,
                CompanyAddress = model.CompanyAddress,
                CompanyWebsite = model.CompanyWebsite,
                ServiceRendering = model.ServiceRendering
            };
            await _marketPlaceRepository.Register(marketPlace);
            return new MarketPlaceResponseModel
            {
               Message = "MarketPlace Created Successfully",
               Status = true,
               Data = new MarketPlaceDto
               {
                Id = marketPlace.Id,
                Image = marketPlace.Image,
                CompanyWebsite = marketPlace.CompanyWebsite,
                CompanyAddress = marketPlace.CompanyAddress,
                CompanyName = marketPlace.CompanyName,
                ServiceRendering = marketPlace.ServiceRendering,
                ServiceType = marketPlace.ServiceType
               }
            };
        }

        public async Task<bool> Delete(string Id)
        {
           var marketPlace = await _marketPlaceRepository.Get(a => a.Id == Id);
           if(marketPlace == null)
           {
               return false;
           } 
            marketPlace.IsDeleted = true;
            await _marketPlaceRepository.Delete(marketPlace);
            return true;
        }

        public async Task<MarketPlacesResponseModel> GetAllMarketPlace()
        {
            var marketPlaces = await _marketPlaceRepository.GetAll();
            var marketPlaceDto = marketPlaces.Select(x => new MarketPlaceDto
            {
                Id = x.Id,
                Image = x.Image,
                CompanyWebsite = x.CompanyWebsite,
                CompanyAddress = x.CompanyAddress,
                CompanyName = x.CompanyName,
                ServiceRendering = x.ServiceRendering,
                ServiceType = x.ServiceType

            }).ToList();

            return new MarketPlacesResponseModel
            {
                Data = marketPlaceDto,
                Message = "List of all MarketPlace",
                Status = true
            };
        }

        public async Task<MarketPlaceResponseModel> GetMarketById(string id)
        {
           var marketPlaces = await _marketPlaceRepository.Get(x => x.Id == id);
            var marketPlaceDto = new MarketPlaceDto
            {
                Id = marketPlaces.Id,
                Image = marketPlaces.Image,
                CompanyWebsite = marketPlaces.CompanyWebsite,
                CompanyAddress = marketPlaces.CompanyAddress,
                CompanyName = marketPlaces.CompanyName,
                ServiceRendering = marketPlaces.ServiceRendering,
                ServiceType = marketPlaces.ServiceType

            };

            return new MarketPlaceResponseModel
            {
                Data = marketPlaceDto,
                Message = "List of all MarketPlace",
                Status = true
            };
        }

        public async Task<MarketPlacesResponseModel> GetMarketPlaceByType(int Type)
        {
            var marketPlaces = await _marketPlaceRepository.GetMarketPlaceByType((ServiceType)Type);
            if(marketPlaces == null) return new MarketPlacesResponseModel
            {
                Message = $"Message With Type {Type} Not Found ",
                Status = false,
            };
             var marketPlaceDto = marketPlaces.Select(x => new MarketPlaceDto
            {
                Id = x.Id,
                Image = x.Image,
                CompanyWebsite = x.CompanyWebsite,
                CompanyAddress = x.CompanyAddress,
                CompanyName = x.CompanyName,
                ServiceRendering = x.ServiceRendering,
                ServiceType = x.ServiceType

            }).ToList();
           
            return new MarketPlacesResponseModel
            {
                Data = marketPlaceDto,
                Message = $"Message With Type {Type} Found ",
                Status = true,
                
            };
        }

        public async Task<BaseResponse> UpdateMarketPlace(UpdateMarketPlaceRequestModel model, string id)
        {
             var marketPlace = await _marketPlaceRepository.Get(a => a.Id == id);
            if(marketPlace == null) return new BaseResponse
            {
                Message = $"MarketPlace With Id {id} Not Found ",
                Status = false,
            };
                marketPlace.Image = model.Image.ToString();
                marketPlace.CompanyName = model.CompanyName;
                marketPlace.CompanyAddress = model.CompanyAddress;
                marketPlace.CompanyWebsite = model.CompanyWebsite;
                marketPlace.ServiceRendering = model.ServiceRendering;

                  var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.Image != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                var filePath = Path.Combine(folderPath, model.Image.FileName);
                var extension = Path.GetExtension(model.Image.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    marketPlace.Image = fileName;
                }
            }
              await _marketPlaceRepository.Update(marketPlace);
            return new BaseResponse
            {
                Message = $"MarketPlace With Id {id} Found ",
                Status = true,
                
            };
        }
    }
}