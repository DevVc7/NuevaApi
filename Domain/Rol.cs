using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public partial class Rol
{

    public int Id { get; set; }


    public string Name { get; set; } = null!;


    public bool Status { get; set; }

    public DateTime? CreateAt { get; set; }


    public DateTime? UpdatedAt { get; set; }

}
