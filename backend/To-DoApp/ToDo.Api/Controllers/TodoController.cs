using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Dtos;
using ToDo.Api.Extensions;
using ToDo.Api.Services;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoDto>>> GetAll(CancellationToken cancellationToken)
        {
            var todos = await _todoService.GetAllAsync(User.GetUserId(), cancellationToken);
            return Ok(todos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var todo = await _todoService.GetByIdAsync(id, User.GetUserId(), cancellationToken);
            if (todo is null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> Create(
            [FromBody] CreateTodoDto dto,
            CancellationToken cancellationToken)
        {
            var todo = await _todoService.CreateAsync(dto, User.GetUserId(), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateTodoDto dto,
            CancellationToken cancellationToken)
        {
            var todo = await _todoService.UpdateAsync(id, dto, User.GetUserId(), cancellationToken);
            if (todo is null) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await _todoService.DeleteAsync(id, User.GetUserId(), cancellationToken);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
