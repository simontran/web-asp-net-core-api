using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiRestful.Domain.Entities.Common;
using WebApiRestful.Domain.Entities.Component;
using WebApiRestful.Service.Component;

namespace WebApiRestful.Infrastructure.Authentication
{
    public class TokenHandler(IConfiguration configuration, IUserService user) : ITokenHandler
    {
        #region Property
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserService _user = user;

        #endregion               

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> CreateToken(User user)
        {
            var tokenSettings = _configuration.GetSection("TokenBearSettings").Get<TokenBear>();

            var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, tokenSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Iss, tokenSettings.Issuer, ClaimValueTypes.String, tokenSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.Integer64, tokenSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Aud, "SuntacWebApi", ClaimValueTypes.Integer64, tokenSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.Now.AddHours(3).ToString("yyyy/MM/dd HH:mm:ss"), ClaimValueTypes.String, tokenSettings.Issuer),
                    new Claim(ClaimTypes.Name, user.UserName.ToString(), ClaimValueTypes.String, tokenSettings.Issuer),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SigningKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenInfo = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                credential
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

            return await Task.FromResult(token);
        }

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="content"></param>
        public async Task ValidateToken(TokenValidatedContext content)
        {
            var claims = content.Principal.Claims.ToList();
            if (claims.Count == 0)
            {
                content.Fail("This token contains no information.");
                return;
            }

            var identity = content.Principal.Identity as ClaimsIdentity;
            if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
            {
                content.Fail("This token is not issued by point entry.");
                return;
            }
            if (identity.FindFirst(ClaimTypes.Name) == null)
            {
                string username = identity.FindFirst(ClaimTypes.Name).Value;

                var user = await _user.GetUser(username);
                if (user == null)
                {
                    content.Fail("This token is invalid for user.");
                    return;
                }
            }
        }
    }
}
