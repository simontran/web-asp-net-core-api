using Microsoft.AspNetCore.Mvc;
using SampleAPI.Component.DomainLayer.Models;
using SampleAPI.Component.ServiceLayer.Services;

namespace SampleAPI.Component.RepositoryLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        #region Property
        private readonly UserService userService;
        #endregion

        public UsersController(UserService service)
        {
            userService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUser model)
        {
            await userService.Create(model);
            return Ok(new { message = "User created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUser model)
        {
            await userService.Update(id, model);
            return Ok(new { message = "User updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.Delete(id);
            return Ok(new { message = "User deleted." });
        }
    }
}
