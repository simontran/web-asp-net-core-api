using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiRestful.Domain.Entities.Common;
using WebApiRestful.Infrastructure.Authentication;

namespace WebApiRestful.Infrastructure.Configuration
{
    public static class TokenBearConfiguration
    {
        public static void RegisterTokenBear(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettings = configuration.GetSection("TokenBearSettings").Get<TokenBear>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = tokenSettings.Issuer,
                        ValidateIssuer = false,
                        ValidAudience = tokenSettings.Audience,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SigningKey)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = content =>
                        {
                            var token = content.HttpContext.RequestServices.GetRequiredService<ITokenHandler>();

                            return token.ValidateToken(content);
                        },
                        OnAuthenticationFailed = content =>
                        {
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = content =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = content =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
