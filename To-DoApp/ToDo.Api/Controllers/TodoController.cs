using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Dtos;
using ToDo.Api.Services;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> Create(
             CreateTodoDto dto,
             CancellationToken cancellationToken)
        {
            var createdTodo = await _todoService.CreateAsync(dto, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var todos = await _todoService.GetAllAsync(cancellationToken);

            return Ok(todos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TodoDto>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var todo = await _todoService.GetByIdAsync(id, cancellationToken);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTodoDto dto,
            CancellationToken cancellationToken)
        {
            var updated = await _todoService.UpdateAsync(id, dto, cancellationToken);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var deleted = await _todoService.DeleteAsync(id, cancellationToken);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
