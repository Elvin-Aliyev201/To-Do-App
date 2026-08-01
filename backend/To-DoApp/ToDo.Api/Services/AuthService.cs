using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Api.Data;
using ToDo.Api.Dtos.Auth;
using ToDo.Api.Entities;

namespace ToDo.Api.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(u => u.Username == dto.Username, cancellationToken);

            if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("İstifadəçi adı və ya şifrə yanlışdır.");
            }

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
        {
             var existingUser = await _context.Users.AnyAsync(u => u.Username == dto.Username, cancellationToken);

            if (existingUser)
            {
                throw new InvalidOperationException("Bu istifadəçi adı artıq mövcuddur.");
            }



            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var token = GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Username = user.Username
            };

        }




        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddDays(
                double.Parse(_configuration["Jwt:ExpiresInDays"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
