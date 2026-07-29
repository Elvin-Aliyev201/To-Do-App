using Microsoft.EntityFrameworkCore;
using ToDo.Api.Data;
using ToDo.Api.Dtos;
using ToDo.Api.Entities;
using ToDo.Api.Mappings;

namespace ToDo.Api.Services
{
    public class TodoService:ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto dto, CancellationToken cancellationToken)
        {
            Todo todo = dto.ToEntity();

            await _context.Todos.AddAsync(todo, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return todo.ToDto();

        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
       .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (todo is null)
            {
                return false;
            }

            _context.Todos.Remove(todo);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<List<TodoDto>> GetAllAsync(CancellationToken cancellationToken)
        {
             var todos = await _context.Todos
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return todos.Select(x => x.ToDto()).ToList();

        }

        public async Task<TodoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (todo is null)
            {
                return null;
            }

            return todo.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, UpdateTodoDto dto, CancellationToken cancellationToken)
        {
              var todo = await _context.Todos
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (todo is null)
            {
                return false;
            }

            dto.UpdateEntity(todo);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
