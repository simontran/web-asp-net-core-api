using Microsoft.AspNetCore.Mvc;
using SampleAPI.Component.ServiceLayer.Services;
using SampleAPI.Component.Users.Models.DTO;

namespace SampleAPI.Component.RepositoryLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        #region Property
        private readonly UserService _service;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public UsersController(UserService service)
        {
            this._service = service;
        }

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
        public async Task<IActionResult> Create(CreateUser model)
        {
            await this._service.Create(model);
            return Ok(new { message = "User created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUser model)
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