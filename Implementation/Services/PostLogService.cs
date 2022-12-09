using System;
using System.Threading.Tasks;
using Unify.UNIFY.Dtos;
using Unify.UNIFY.Interfaces.Repository;
using Unify.UNIFY.Interfaces.Services;
using Unify.UNIFY.Model.Entities;
using UNIFY.Dtos;

namespace Unify.UNIFY.Implementation.Services
{
    public class PostLogService : IPostLogService
    {
        private readonly IPostLogRepository _postLogRepository;

        public PostLogService(IPostLogRepository postLogRepository)
        {
            _postLogRepository = postLogRepository;
        }

        public async Task<BaseResponse> CreatePostLog(CreatePostLogRequestModel model)
        {
              var postLog = new PostLog
            {
                PostId= model.PostId,
                PostUrl= model.PostUrl,
                DateCreated = DateTime.UtcNow
            };
            await _postLogRepository.Register(postLog);
            return new BaseResponse
            {
              Message = "Created post Log Successfully",
             Status=true,
             
                
            };
        }
    }
}