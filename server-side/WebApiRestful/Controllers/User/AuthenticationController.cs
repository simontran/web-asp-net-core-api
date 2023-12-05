using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiRestful.Domain.Models;
using WebApiRestful.Infrastructure.Authentication;
using WebApiRestful.Service.Component;

namespace WebApiRestful.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IUserService service, ITokenHandler token) : ControllerBase
    {
        #region Property
        private readonly IUserService _service = service;
        private readonly ITokenHandler _token = token;

        #endregion

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountModel model)
        {
            if (model == null) return BadRequest(new { message = "User is not exist." });

            var user = await _service.CheckLogin(model.UserName, model.Password);
            if (user == null) return Unauthorized();

            return await Task.Factory.StartNew(() =>
            {
                return Ok(_token.CreateToken(user));
            });
        }
    }
}
