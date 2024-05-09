using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace administrarNegocios_A_B_POS_Solutions;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Nombres { get; set; }
    [Required]
    [MaxLength(50)]
    public string Apellidos { get; set; }
    [Required]
    public int TipoUsuarioId { get; set; }
    [ForeignKey("TipoUsuarioId")]
    public TipoUsuario TipoUsuario { get; set; }
    [Required]
    [MaxLength(200)]
    public string Correo { get; set; }
    [Required]
    [MaxLength(50)]
    public string Contraseña { get; set; }
}
