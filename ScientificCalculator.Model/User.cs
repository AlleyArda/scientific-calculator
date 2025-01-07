namespace ScientificCalculator.Model;

public class User
{
    public int Id { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public ICollection<Calculation> Calculations { get; set; } = new List<Calculation>();

    public bool IsAdmin { get; set; }
}