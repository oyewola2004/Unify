using Org.BouncyCastle.Crypto.Generators;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using static UNIFY.Dtos.UserDto;

namespace UNIFY.Implementation.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMemberRepository _memberRepository;
        public UserService(IUserRepository userRepository, IMemberRepository memberRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _memberRepository = memberRepository;
        }
        public async Task<UserResponseModel> Login(UserRequestModel model)
        {
            System.Console.WriteLine(model.Email);
            var user = await _userRepository.Get(x => x.Email == model.Email);
            var member = await _memberRepository.Get(x => x.UserId == user.Id);
            if(user == null )
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }
            else if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var roles = _roleRepository.GetRolesByUserId(user.Id);
                if(roles.Where(x => x.RoleName == "member").Count() > 0)
                {
                    if(member.IsVerified != true)
                    {
                         return new UserResponseModel
                        {
                            Message = "You are not verified, pls wait for admin verification",
                            Status = false,
                        };
                    }
                }
                return new UserResponseModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.Select(x => new RoleDto
                    {
                        Name = x.RoleName,
                        Description = x.Description
                    }).ToList(),
                    Message = "Successfully logged in",
                    Status = true
                };
            }
             else
            {
               return new UserResponseModel
               {
                   Message = "Invalid Credentials",
                   Status = false
               };
            }
        }
        
    }
}
