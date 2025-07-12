using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public partial class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Sex { get; set; }
    public string? Photo { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Address { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
