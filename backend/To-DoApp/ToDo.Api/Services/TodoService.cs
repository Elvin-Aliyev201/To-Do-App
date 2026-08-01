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


        public async Task<List<TodoDto>> GetAllAsync(int userId, CancellationToken cancellationToken)
        {
            return await _context.Todos
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .Select(t => t.ToDto())
                .ToListAsync(cancellationToken);
        }

        public async Task<TodoDto?> GetByIdAsync(int id, int userId, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);

            return todo?.ToDto();
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto dto, int userId, CancellationToken cancellationToken)
        {
            var todo = dto.ToEntity();
            todo.UserId = userId;
            todo.CreatedAt = DateTime.UtcNow;

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return todo.ToDto();
        }

        public async Task<TodoDto?> UpdateAsync(int id, UpdateTodoDto dto, int userId, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);

            if (todo is null) return null;

            todo.Title = dto.Title;
            todo.IsCompleted = dto.IsCompleted;

            await _context.SaveChangesAsync(cancellationToken);

            return todo.ToDto();
        }

        public async Task<bool> DeleteAsync(int id, int userId, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);

            if (todo is null) return false;

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
