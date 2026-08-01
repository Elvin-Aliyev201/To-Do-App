using ToDo.Api.Dtos;

namespace ToDo.Api.Services
{
    public interface ITodoService
    {
        Task<List<TodoDto>> GetAllAsync(int userId, CancellationToken cancellationToken);
        Task<TodoDto?> GetByIdAsync(int id, int userId, CancellationToken cancellationToken);
        Task<TodoDto> CreateAsync(CreateTodoDto dto, int userId, CancellationToken cancellationToken);
        Task<TodoDto?> UpdateAsync(int id, UpdateTodoDto dto, int userId, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, int userId, CancellationToken cancellationToken);
    }
}
