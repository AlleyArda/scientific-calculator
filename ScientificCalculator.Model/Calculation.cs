namespace ScientificCalculator.Model;

public class Calculation
{
    public int Id { get; set; }
    public string Expression { get; set; } = String.Empty;
    public string Result { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Key
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}