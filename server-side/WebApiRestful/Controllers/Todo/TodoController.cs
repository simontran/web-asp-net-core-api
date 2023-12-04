using Microsoft.AspNetCore.Mvc;
using WebApiRestful.Domain.Models;
using WebApiRestful.Service.Component;

namespace WebApiRestful.Controllers.Todo
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(ITodoService service) : ControllerBase
    {
        #region Property
        private readonly ITodoService _service = service;

        #endregion

        [HttpGet]   // api/todo
        public async Task<IActionResult> GetTodoAll()
        {
            var todos = await this._service.GetTodoAll();
            return Ok(todos);
        }

        [HttpGet("{id:int}")]   // api/todo/3
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await this._service.GetTodoById(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTodo(TodoCreate model)
        {
            await this._service.AddNewTodo(model);
            return Ok(new { message = "Todo created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoUpdate model)
        {
            await this._service.UpdateTodo(id, model);
            return Ok(new { message = "Todo updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            await this._service.DeleteTodo(id);
            return Ok(new { message = "Todo deleted." });
        }
    }
}
