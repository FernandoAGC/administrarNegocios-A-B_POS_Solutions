using System.ComponentModel.DataAnnotations;

namespace administrarNegocios_A_B_POS_Solutions;

public class TipoUsuario
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombre { get; set; }
}
