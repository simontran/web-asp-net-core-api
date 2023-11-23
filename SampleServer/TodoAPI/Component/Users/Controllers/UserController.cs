using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.ServiceLayer.Services;

namespace SampleAPI.Component.RepositoryLayer.Controllers
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="service"></param>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService service) : ControllerBase
    {
        #region Property
        private readonly UserService _service = service;

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await this._service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await this._service.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreate model)
        {
            await this._service.Create(model);
            return Ok(new { message = "User created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdate model)
        {
            await this._service.Update(id, model);
            return Ok(new { message = "User updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this._service.Delete(id);
            return Ok(new { message = "User deleted." });
        }
    }
}