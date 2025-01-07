using ScientificCalculator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificCalculator.Services.Interfaces;

public interface ICalculationService
{
    Task<Calculation> AddCalculationAsync(int userId, string expression);
    
    Task<IEnumerable<Calculation>> GetUserCalculationsAsync(int userId);
    
    Task DeleteCalculationAsync(int calculationId , int currentUserId);
}