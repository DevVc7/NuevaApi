
namespace Domain;
public class User
{
    public int IdUsuario { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? Correo { get; set; }
    public string? Password { get; set; }
    public string? NroDocumento { get; set; }
    public bool Estado { get; set; }
    public string? Biografia { get; set; }
    public string? Photo { get; set; }
    public int? IdEscuela { get; set; }
    public virtual Escuela? Escuela { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int? DeletedBy { get; set; }
}
