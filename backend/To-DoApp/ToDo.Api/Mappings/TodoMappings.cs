using ToDo.Api.Dtos;
using ToDo.Api.Entities;

namespace ToDo.Api.Mappings
{
    public static class TodoMappings
    {
        public static Todo ToEntity(this CreateTodoDto dto)
        {
            return new Todo
            {
                Title = dto.Title,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static TodoDto ToDto(this Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                CreatedAt = todo.CreatedAt
            };
        }

        public static void UpdateEntity(this UpdateTodoDto dto, Todo todo)
        {
            todo.Title = dto.Title;
            todo.IsCompleted = dto.IsCompleted;
        }
    }
}
