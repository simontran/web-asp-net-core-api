using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiRestful.Domain.Models;
using WebApiRestful.Service.Component;

namespace WebApiRestful.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        #region Property
        private readonly IUserService _service = service;

        #endregion

        [HttpPost]
        public async Task<IActionResult> AddNewUser(CreateUserModel model)
        {
            await this._service.AddNewUser(model);
            return Ok(new { message = "User created." });
        }
    }
}
