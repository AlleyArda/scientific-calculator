using Microsoft.AspNetCore.Mvc;
using ScientificCalculator.Services.Interfaces;
using System;
using ScientificCalculator.Common.DTO;

namespace ScientificCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationService _calculationService;

        public CalculationController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        /// <summary>
        /// Kullanıcının gönderdiği matematiksel ifadeyi hesaplar, veritabanına kaydeder ve sonucu döndürür.
        /// Örnek: POST /api/calculation?userId=1&expression=3+5*2-sin(45)
        /// </summary>
        /// <param name="userId">Bu işlemi gerçekleştiren kullanıcı Id'si</param>
        /// <param name="expression">Matematiksel ifade (ör: "3+5", "sqrt(16)+log(10)")</param>
        /// <returns>Oluşturulan Calculation kaydı veya 400 Hata</returns>
        [HttpPost("calculate")]
        public async Task<IActionResult> AddCalculation([FromBody] CalculationDto dto)
        {
            try
            {
                var userIdStr = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userIdStr))
                    return Unauthorized();

                int userId = int.Parse(userIdStr);
                var calc = await _calculationService.AddCalculationAsync(userId, dto.Expression);
                return Ok(calc);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Kullanıcının geçmişte yaptığı hesaplamaları döndürür.
        /// Örnek: GET /api/calculation/user/1
        /// </summary>
        /// <param name="userId">Hesaplamaları istenen kullanıcı Id'si</param>
        /// <returns>Kullanıcının Calculation kayıtları</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCalculations(int userId)
        {
            var calculations = await _calculationService.GetUserCalculationsAsync(userId);
            return Ok(calculations);
        }

        /// <summary>
        /// Belirli bir hesaplama kaydını siler (Sadece sahibi silebilsin).
        /// Örnek: DELETE /api/calculation/5?userId=1
        /// </summary>
        /// <param name="calculationId">Silinmek istenen Calculation'ın Id'si</param>
        /// <param name="userId">Şu anki kullanıcı Id'si</param>
        /// <returns>204 NoContent veya 403 Forbidden</returns>
        [HttpDelete("{calcId}")]
public async Task<IActionResult> DeleteCalc(int calcId)
{
    // userId’yi session’dan veya token’dan al
    var userIdStr = HttpContext.Session.GetString("UserId"); 
    if (string.IsNullOrEmpty(userIdStr)) return Unauthorized(new { message = "Please login first" });

    try
    {
        int userId = int.Parse(userIdStr);
        await _calculationService.DeleteCalculationAsync(calcId, userId);
        return Ok(new { message = "Calculation deleted" });
    }
    catch (UnauthorizedAccessException ex)
    {
        return Forbid(ex.Message); // 403
    }
}
        
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return Unauthorized();

            int userId = int.Parse(userIdStr);
            var list = await _calculationService.GetUserCalculationsAsync(userId);
            return Ok(list);
        }
        
    }
}