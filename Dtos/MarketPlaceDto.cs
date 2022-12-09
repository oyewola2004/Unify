using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Model.Enums;
using UNIFY.Dtos;

namespace Dtos
{
    public class MarketPlaceDto
    {
        public string Id {get; set; }
        public string Image {get; set; }
        public string CompanyName {get; set; }
        public string CompanyAddress {get; set; }
        public string CompanyWebsite {get; set; }
        public string ServiceRendering {get; set;}
        public ServiceType ServiceType {get; set; }
    }

    public class CreateMarketPlaceRequestModel
    {
        
        public string Image {get; set; }
        public string CompanyName {get; set; }
        public string CompanyAddress {get; set; }
        public string CompanyWebsite {get; set; }
        public string ServiceRendering {get; set;}
        public ServiceType ServiceType {get; set; }
    }

    public class UpdateMarketPlaceRequestModel
    {
        public IFormFile Image {get; set; }
        public string CompanyName {get; set; }
        public string CompanyAddress {get; set; }
        public string CompanyWebsite {get; set; }
        public string ServiceRendering {get; set;}
    }


     public class MarketPlaceResponseModel : BaseResponse
    {
        public MarketPlaceDto Data { get; set; }
    }
    public class MarketPlacesResponseModel : BaseResponse
    {
        public ICollection<MarketPlaceDto> Data { get; set; } = new HashSet<MarketPlaceDto>();
    }
}