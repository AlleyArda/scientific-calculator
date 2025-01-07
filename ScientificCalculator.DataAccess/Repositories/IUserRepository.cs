using Microsoft.EntityFrameworkCore;
using ScientificCalculator.Data;
using ScientificCalculator.DataAccess.Interfaces;
using ScientificCalculator.Model;

namespace ScientificCalculator.DataAccess.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ScientificCalculatorContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}