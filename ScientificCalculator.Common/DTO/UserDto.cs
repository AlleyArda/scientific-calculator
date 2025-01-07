namespace ScientificCalculator.Common.DTO;
//bir kullanıcı bilgisi dönerken sadece gerekli olan bilgileri döndürmek için kullanılır
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;

    public bool IsAdmin { get; set; }
}