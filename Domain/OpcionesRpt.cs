using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain;

public class OpcionesRpt
{
    public int IdOpcion {  get; set; }
    public int IdPregunta { get; set; }
    public string? Texto { get; set; }
    public bool EsCorrecta { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(IdPregunta))]
    public virtual Pregunta? Pregunta { get; set; }

}
