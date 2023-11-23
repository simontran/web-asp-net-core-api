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
    public class TodoController(TodoService service) : ControllerBase
    {
        #region Property
        private readonly TodoService _service = service;

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await this._service.GetAll();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await this._service.GetById(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreate model)
        {
            await this._service.Create(model);
            return Ok(new { message = "Todo created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TodoUpdate model)
        {
            await this._service.Update(id, model);
            return Ok(new { message = "Todo updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this._service.Delete(id);
            return Ok(new { message = "Todo deleted." });
        }
    }
}