using Blue.Challenge.Business.Commands;
using Blue.Challenge.Business.Responses;
using Blue.Challenge.Business.Responses.Commands;
using Blue.Challenge.Business.Validators;
using Blue.Challenge.Domain.Configuration;
using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Blue.Challenge.Business.Handlers.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, RequestHandlerResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtConfiguration _jwtConfiguration;
        public LoginCommandHandler(IUserRepository userRepository,
                                   IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtConfiguration = configuration.GetSection("Jwt").Get<JwtConfiguration>();
        }

        public async Task<RequestHandlerResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var validator = new LoginCommandValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                return new RequestHandlerResponse(validation.Errors, HttpStatusCode.BadRequest);
            }
            var user = await _userRepository.GetByEmail(request.Email);
            if (user is null)
            {
                return new RequestHandlerResponse("Email inválido", HttpStatusCode.BadRequest);
            }
            if (!user.VerifyPassword(request.Password))
                return new RequestHandlerResponse("Senha incorreta", HttpStatusCode.BadRequest);

            var login = CreateLogin(user);
            return new RequestHandlerResponse(login, HttpStatusCode.OK);
        }

        private LoginCommandResponse CreateLogin(User user)
        {
            var expirationDate = DateTime.Now.AddDays(1);
            var secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.Secret));
            var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>
            {
                new Claim(nameof(User.UserId), user.UserId.ToString()),
                new Claim(nameof(User.Email), user.Email),
                new Claim(nameof(User.Name), user.Name)
            };
            var jwt = new JwtSecurityToken(notBefore: DateTime.Now,
                                           expires: expirationDate,
                                           signingCredentials: signingCredentials,
                                           claims: claims);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new LoginCommandResponse { UserName = user.Name, AccessToken = token, AccessTokenExpireDate = expirationDate, IssuedAt = DateTime.Now, UserId = user.UserId };

        }
    }
}
