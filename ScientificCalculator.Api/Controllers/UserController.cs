using Microsoft.AspNetCore.Mvc;
using ScientificCalculator.Common.DTO;
using ScientificCalculator.Services.Interfaces;


namespace ScientificCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Burada RegisterRequest => userService.RegisterUser(...) şeklinde kullanabiliriz
            var user = await _userService.RegisterUserAsync(request.Username, request.Email, request.Password);
            // user döndüğünde entity mi döndürüyoruz? Dönüşte UserDto'ya map'lemek istersen
            // basitçe el ile:
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                
            };
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Kullanıcıyı giriş yapmaya çalış
            var user = await _userService.LoginAsync(request.Username, request.Password);

            // Eğer kullanıcı bulunamazsa
            if (user == null) 
                return Unauthorized("Invalid credentials");

            // Oturum verilerini ayarla
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.IsAdmin ? "Admin" : "User");

            // Başarılı giriş
            return Ok(new
            {
                message = $"Login successful. Welcome {user.Username}!",
                role = user.IsAdmin ? "Admin" : "User"
            });
        }
        
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var username = HttpContext.Session.GetString("Username");
            HttpContext.Session.Clear();
            //return ok and Username
            return Ok($"Goodbye {username}");
            
            
        }
    }
}