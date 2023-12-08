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
                    // primary key token
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, tokenSettings.Issuer),
                    // token issuer -> server
                    new Claim(JwtRegisteredClaimNames.Iss, tokenSettings.Issuer, ClaimValueTypes.String, tokenSettings.Issuer),
                    // allocation time
                    //new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), ClaimValueTypes.Integer64, tokenSettings.Issuer),
                    // author
                    new Claim(JwtRegisteredClaimNames.Aud, "TodoWebApi", ClaimValueTypes.String, tokenSettings.Issuer),
                    // expiry
                    new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes((double)tokenSettings.DurationInMinutes).ToString("yyyy/MM/dd HH:mm:ss"), ClaimValueTypes.String, tokenSettings.Issuer),
                    // Username
                    new Claim(ClaimTypes.Name, user.UserName.ToString(), ClaimValueTypes.String, tokenSettings.Issuer),
            };

            // primary key token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SigningKey ?? "secret-key"));
            // security algorithms
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // info
            var tokenInfo = new JwtSecurityToken(
                issuer: tokenSettings.Issuer,
                audience: tokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes((double)tokenSettings.DurationInMinutes),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
                );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenInfo));
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

            if (identity.FindFirst(JwtRegisteredClaimNames.Exp) == null)
            {
                var dateExp = identity.FindFirst(JwtRegisteredClaimNames.Exp);
            }
        }
    }
}
