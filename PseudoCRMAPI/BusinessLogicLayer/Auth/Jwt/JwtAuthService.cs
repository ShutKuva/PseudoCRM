using BusinessLogicLayer.Abstractions.Auth;
using Core;
using Core.Auth.Jwt;
using Core.Auth.Jwt.Parameters;
using Core.Auth.Jwt.Results;
using Core.Dtos.User;
using DataAccessLayer.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Services;

namespace BusinessLogicLayer.Auth.Jwt
{
    public class JwtAuthService : IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IOrganizationService<Organization> _organizationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtConfiguration _jwtOptions;

        public JwtAuthService(IRepository<User> userRepository, IOrganizationService<Organization> organizationService, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtConfiguration> jwtOptions)
        {
            _userRepository = userRepository;
            _organizationService = organizationService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public Task<JwtResult> Login(JwtAuthLoginParameters parameters)
        {
            if (parameters.User != null)
            {
                return LoginWithCredentials(parameters.User);
            } 
            if (parameters.OldResult != null)
            {
                return RefreshToken(parameters.OldResult);
            }
            throw new ArgumentException("It is necessary to specify method of generating token");
        }

        public async Task<JwtResult> Register(JwtAuthRegistrationParameters parameters)
        {
            User user = _mapper.Map<User>(parameters.User);

            Organization? organization = await _organizationService.ReadAsync(org => org.Name == parameters.User.OrganizationName, 0, 0);

            if (organization == null && parameters.User.OrganizationName != null)
            {
                organization = new Organization() { Name = parameters.User.OrganizationName };

                await _organizationService.CreateAsync(organization);

                user.OrganizationId = organization.Id;
            }

            await _userRepository.CreateAsync(user);

            await _unitOfWork.SaveChangesAsync();

            JwtResult result = await GenerateJwtResult(user);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        private async Task<JwtResult> LoginWithCredentials(UserLoginDto user)
        {
            User? userObj = await _userRepository.ReadAsync(u => u.Name == user.Name && u.PasswordHash == user.PasswordHash, 0, 0);
            if (userObj == null)
            {
                throw new ArgumentException("There is no user with this credentials");
            }

            JwtResult result = await GenerateJwtResult(userObj);

            return result;
        }

        private async Task<JwtResult> RefreshToken(JwtResult oldResult)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(oldResult.Token);

            int id = int.Parse(token.Claims.FirstOrDefault(c => c.Type == ClaimNames.Id).Value);
            User? user = await _userRepository.ReadAsync(u => u.Id == id, 0, 0);
            if (user == null)
            {
                throw new ArgumentException("There is no user with this credentials");
            }

            if (user.RefreshToken != oldResult.RefreshToken)
            {
                throw new ArgumentException("Invalid refresh token");
            }

            JwtResult result = await GenerateJwtResult(user);

            user.RefreshToken = result.RefreshToken;

            await _userRepository.UpdateAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        private async Task<JwtResult> GenerateJwtResult(User user)
        {
            JwtResult result = new JwtResult();
            result.Token = GenerateToken(user);
            result.RefreshToken = GenerateRefreshToken();

            return result;
        }

        private string GenerateToken(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor();

            descriptor.Claims = new Dictionary<string, object>
            {
                [ClaimNames.Name] = user.Name,
                [ClaimNames.Id] = user.Id,
            };

            descriptor.Issuer = _jwtOptions.Issuer;
            descriptor.Audience = _jwtOptions.Audience;
            descriptor.Expires = DateTime.Now.AddHours(_jwtOptions.Expires);
            descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256Signature);

            return handler.CreateEncodedJwt(descriptor);
        }

        private string GenerateRefreshToken()
        {
            Random r = new Random();
            byte[] byteArr = new byte[64];
            r.NextBytes(byteArr);
            return Encoding.ASCII.GetString(byteArr);
        }
    }
}