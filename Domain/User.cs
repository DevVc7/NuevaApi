
namespace Domain;
public class User
{
    public Guid? Id { get; set; }
    public string? Email { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? Rol { get; set; }
    public string? Name { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
