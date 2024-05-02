using UserManagement.Models;

namespace UserManagement.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(String userName, String password);
    }
}
