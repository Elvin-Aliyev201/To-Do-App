using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.Dtos
{
    public class CreateTodoDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;
    }
}
