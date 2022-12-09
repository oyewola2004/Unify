using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using UNIFY.Dtos;

namespace Unify.UNIFY.Dtos
{
    public class SecurityAgencyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Abbreviation { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public IList<SecurityDto> Securities { get; set; }

    }

     public class CreateSecurityAgencyRequestModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public IFormFile? Logo { get; set; }
    }

    public class UpdateSecurityAgencyRequestModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public IFormFile? Logo { get; set; }
    }

     public class SecurityAgencyResponseModel : BaseResponse
        {
            public SecurityAgencyDto Data { get; set; }
        }
        public class SecurityAgenciesResponseModel : BaseResponse
        {
            public ICollection<SecurityAgencyDto> Data { get; set; } = new HashSet<SecurityAgencyDto>();
        }
}