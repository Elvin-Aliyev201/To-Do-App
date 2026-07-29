using ToDo.Api.Dtos;

namespace ToDo.Api.Services
{
    public interface ITodoService
    {
        Task<TodoDto> CreateAsync(
       CreateTodoDto dto,
       CancellationToken cancellationToken);

        Task<List<TodoDto>> GetAllAsync(
            CancellationToken cancellationToken);

        Task<TodoDto?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);

        Task<bool> UpdateAsync(
            int id,
            UpdateTodoDto dto,
            CancellationToken cancellationToken);

        Task<bool> DeleteAsync(
            int id,
            CancellationToken cancellationToken);
    }
}
