using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCalc;
using ScientificCalculator.DataAccess.Repositories;
using ScientificCalculator.Services.Interfaces;
using ScientificCalculator.Model;

namespace ScientificCalculator.Services.Concrete
{
    public class CalculationService : ICalculationService
    {
        private readonly ICalculationRepository _calculationRepository;

        public CalculationService(ICalculationRepository calculationRepository)
        {
            _calculationRepository = calculationRepository;
        }

        /// <summary>
        /// Kullanıcının girdiği matematiksel ifadeyi (expression) değerlendirip sonucunu hesaplar,
        /// Calculation tablosuna kaydeder ve geriye kaydı döndürür.
        /// </summary>
        /// <param name="userId">Bu işlemi yapan kullanıcının Id'si</param>
        /// <param name="expression">Matematiksel ifade (örn: "3+5*2-sin(45)+sqrt(16)")</param>
        /// <returns>Oluşturulan Calculation kaydı (Id, Expression, Result, CreatedAt vb.)</returns>
        public async Task<Calculation> AddCalculationAsync(int userId, string expression)
        {
            // 1) İfade sonucunu hesapla
            var evaluatedResult = EvaluateExpression(expression);

            // 2) Hesaplamayı veritabanına kaydet
            var calc = new Calculation
            {
                UserId = userId,
                Expression = expression,
                Result = evaluatedResult,
                CreatedAt = DateTime.UtcNow
            };

            await _calculationRepository.AddAsync(calc);
            await _calculationRepository.SaveChangesAsync();

            return calc;
        }

        /// <summary>
        /// Belirli bir kullanıcının tüm geçmiş hesaplamalarını (Calculation) döndürür.
        /// </summary>
        public async Task<IEnumerable<Calculation>> GetUserCalculationsAsync(int userId)
        {
            return await _calculationRepository.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// NCalc kütüphanesini kullanarak matematiksel ifadeyi değerlendirir.
        /// Hatalı ifade girildiğinde ArgumentException fırlatır.
        /// </summary>
        /// <param name="expression">Kullanıcıdan gelen matematiksel ifade</param>
        /// <returns>İfadenin string olarak hesaplanmış sonucu</returns>
        private string EvaluateExpression(string expression)
        {
            try
            {
                // NCalc nesnesi oluştur
                var e = new Expression(expression);

                // Değerlendir (Evaluate). Dönüş değeri object (double, int, bool vb. olabilir)
                var value = e.Evaluate();

                // Sonucu string'e çevirerek veritabanına kaydedeceğiz
                return value?.ToString() ?? "0";
            }
            catch (Exception ex)
            {
                // İfadede sentaks hatası vb. durumlarda NCalc exception fırlatır
                throw new ArgumentException($"Geçersiz bir ifade girdiniz: {expression}", ex);
            }
        }
        
        public async Task DeleteCalculationAsync(int calculationId, int currentUserId)
        {
            var calc = await _calculationRepository.GetByIdAsync(calculationId);
            if (calc == null) 
                return; // Hesaplama yoksa sessizce çıkabiliriz veya NotFoundException atabiliriz

            if (calc.UserId != currentUserId)
            {
                // Farklı bir kullanıcıya aitse silmeye yetkisi yok
                throw new UnauthorizedAccessException("You do not own this calculation.");
            }

            // Sahibi doğruysa sil
            _calculationRepository.Delete(calc);
            await _calculationRepository.SaveChangesAsync();
        }
        
        
    }
}