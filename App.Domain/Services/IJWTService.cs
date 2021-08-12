using App.Domain.Commands.Response;
using App.Domain.Entities;

namespace App.Domain.Services
{
    public interface IJWTService
    {
        string GenerateBearerToken(UserResponse user);
    }
}
