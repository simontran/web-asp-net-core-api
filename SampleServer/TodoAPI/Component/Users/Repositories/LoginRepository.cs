using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Core.Common.Helpers;
using TodoAPI.Core.DomainLayer.Data;
using TodoAPI.Core.RepositoryLayer.Repositories;

namespace TodoAPI.Component.RepositoryLayer.Repository
{
    public class LoginRepository(DataContext context, IOptions<JwtSettings> jwtSettings) : Repository<User>(context)
    {
        #region Property
        private readonly DataContext _context = context;
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        #endregion

        /// <summary>
        /// Get Password By UserName
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<string> GetPassword(string username)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT PasswordHash ");
            sql.AppendLine("  FROM M_USER ");
            sql.AppendLine(" WHERE UserName = @UserName ");
            return await connection?.QuerySingleOrDefaultAsync<string>(sql.ToString(), new { username });
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Login(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Key ?? "secretkey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName ?? "unknown"),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(
                this._jwtSettings.Issuer,
                this._jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}