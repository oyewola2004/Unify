using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Unify.UNIFY.Dtos;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Services
{
    public class SecurityAgencyService : ISecurityAgencyService
    {

         private readonly ISecurityAgencyRepository _agencyRepository;
        private readonly IWebHostEnvironment _webroot;

        public SecurityAgencyService(ISecurityAgencyRepository agencyRepository, IWebHostEnvironment webroot)
        {
            _agencyRepository = agencyRepository;
            _webroot = webroot;
        }
        public async Task<BaseResponse> Create(CreateSecurityAgencyRequestModel model)
        {
             var agencyExist = await _agencyRepository.Get(a => a.Name == model.Name);
            if (agencyExist != null) return new BaseResponse
            {
                Message = "Agency already exist",
                Status = false,
            };
             var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.Logo != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.Logo.FileName);
                var filePath = Path.Combine(folderPath, model.Logo.FileName);
                var extension = Path.GetExtension(model.Logo.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Logo.CopyToAsync(stream);
                    }
                    agencyExist.Logo = fileName;
                }
            }

            var agency = new SecurityAgency
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                RegistrationNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),
                Description = model.Description,
                Logo = model.Logo.ToString(),

            };

            await _agencyRepository.Register(agency);

            return new BaseResponse
            {
                Message = "Agency created successfully",
                Status = true,
            };
        }

        public async Task<BaseResponse> Delete(string id)
        {
            var agency = await _agencyRepository.Get(id);
            if (agency == null) return new BaseResponse
            {
                Message = "Role Not Found",
                Status = false,
            };

            agency.IsDeleted = true;
            await _agencyRepository.Delete(agency);

            return new BaseResponse
            {
                Message = "Delete Successful",
                Status = true,
            };
        }

        public async Task<BaseResponse> Get(string id)
        {
             var agency = await _agencyRepository.Get(id);
            if (agency == null) return new BaseResponse
            {
                Message = "Agency not found",
                Status = false,
            };
             var securityAgencyDto = new SecurityAgencyDto
             {
                  Id = agency.Id,
                    Name = agency.Name,
                    Abbreviation = agency.Abbreviation,
                    RegistrationNumber = agency.RegistrationNumber,
                    Description = agency.Description,
                    Logo = agency.Logo,
             };
            return new BaseResponse
            {
                Message = "Successful",
                Status = true,
              
            };
        }

        public async Task<BaseResponse> GetAll()
        {
             var agency = await _agencyRepository.GetAll();
            if (agency == null) return new BaseResponse
            {
                Message = "Agency not found",
                Status = false,
            };
             var securityAgencyDto = new SecurityAgencyDto
             {
                  
             };
            return new BaseResponse
            {
                Message = "Successful",
                Status = true,
              
            };
            
           
        }

        public async Task<BaseResponse> Update(string id, UpdateSecurityAgencyRequestModel model)
        {
            var agency = await _agencyRepository.Get(id);

            if (agency == null) return new BaseResponse
            {
                Message = "Agency not found",
                Status = false,
            };

           var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.Logo != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.Logo.FileName);
                var filePath = Path.Combine(folderPath, model.Logo.FileName);
                var extension = Path.GetExtension(model.Logo.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Logo.CopyToAsync(stream);
                    }
                    agency.Logo = fileName;
                }
            }

            agency.Name = model.Name;
            agency.Abbreviation = model.Abbreviation;
            agency.Description = model.Description;
            agency.Logo = model.Logo.ToString();

            await _agencyRepository.Update(agency);
            return new BaseResponse
            {
                Message = "Updated Succesfully",
                Status = true,
                
            };
        }
    }
}
