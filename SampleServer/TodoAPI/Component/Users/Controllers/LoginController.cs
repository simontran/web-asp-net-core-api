using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.ServiceLayer.Services;

namespace SampleAPI.Component.RepositoryLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(LoginService service) : ControllerBase
    {
        #region Property
        private readonly LoginService _service = service;

        #endregion

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var token = await this._service.Login(model);
            if (token != string.Empty)
                return Ok(token);
            else
                return NotFound(new { message = "User not found." });
        }
    }
}