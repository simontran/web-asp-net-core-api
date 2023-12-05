using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApiRestful.Infrastructure.Authentication
{
    public class TokenHandlerBase
    {

        public void ValidateToken(TokenValidatedContext content)
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
                content.Fail("This token is not issued by point entry.");
                return;
            }
        }
    }
}