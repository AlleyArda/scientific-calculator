namespace ScientificCalculator.Common.DTO;
//DTO stands for Data Transfer Object
public class RegisterRequest
{
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}
