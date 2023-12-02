using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoWebApiRestful.Common.PersistenceLayer.Repositories;
using TodoWebApiRestful.Component.DomainLayer.Dto;
using TodoWebApiRestful.Component.DomainLayer.Entities;
using TodoWebApiRestful.Component.ServiceLayer.Services;

namespace TodoWebApiRestful.Component.PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase //TodoService service
    {
        #region Property
        //private readonly TodoService _service = service;
        private readonly IUnitOfWork _service = unitOfWork;
        private readonly IMapper _mapper = mapper;

        #endregion

        [HttpGet]   // api/todo
        public async Task<IActionResult> GetAll()
        {
            //var todos = await this._service.GetAll();
            var todos = await this._service.Todos.GetAllAsync();
            return Ok(todos);
        }

        [HttpGet("{id:int}")]   // api/todo/3
        public async Task<IActionResult> GetById(int id)
        {
            //var todo = await this._service.GetById(id);
            var todo = await this._service.Todos.GetByIdAsync(id);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoCreate model)
        {
            //await this._service.Create(model);
            // Ｍap model to new todo object
            var todo = this._mapper.Map<Todo>(model);
            await this._service.Todos.CreateAsync(todo);
            return Ok(new { message = "Todo created." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TodoUpdate model)
        {
            //await this._service.Update(id, model);
            // Copy model props to user
            var todo = new Todo();
            this._mapper.Map(model, todo);
            await this._service.Todos.UpdateAsync(todo);
            return Ok(new { message = "Todo updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //await this._service.Delete(id);
            await this._service.Todos.DeleteAsync(id);
            return Ok(new { message = "Todo deleted." });
        }
    }
}
