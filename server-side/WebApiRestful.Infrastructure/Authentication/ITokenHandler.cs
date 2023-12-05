using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Infrastructure.Authentication
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
        Task ValidateToken(TokenValidatedContext content);
    }
}