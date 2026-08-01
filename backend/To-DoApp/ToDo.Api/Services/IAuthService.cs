using ToDo.Api.Dtos.Auth;

namespace ToDo.Api.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);
        Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken);
    }
}
