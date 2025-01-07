using ScientificCalculator.Model;

namespace ScientificCalculator.Services.Interfaces;

public interface IUserService
{
    Task<User> RegisterUserAsync(string username, string email, string password);
    Task<User?> LoginAsync(string username, string password);
    Task<User?> GetUserByIdAsync(int id);
}