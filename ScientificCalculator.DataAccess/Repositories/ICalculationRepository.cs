using Microsoft.EntityFrameworkCore;
using ScientificCalculator.Data;
using ScientificCalculator.DataAccess.Interfaces;
using ScientificCalculator.Model;

namespace ScientificCalculator.DataAccess.Repositories;

public interface ICalculationRepository : IGenericRepository<Calculation>
{
    Task<IEnumerable<Calculation>> GetByUserIdAsync(int userId);
}

public class CalculationRepository : GenericRepository<Calculation>, ICalculationRepository
{
    public CalculationRepository(ScientificCalculatorContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Calculation>> GetByUserIdAsync(int userId)
    {
        return await _dbSet.Where(c => c.UserId == userId).ToListAsync();
    }
}