using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace administrarNegocios_A_B_POS_Solutions;

public class Negocio
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }
    [MaxLength(500)]
    public string Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    [Required]
    public int UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }
}
