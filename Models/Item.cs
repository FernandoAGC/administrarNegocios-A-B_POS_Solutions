using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace administrarNegocios_A_B_POS_Solutions;

public class Item
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; }
    [MaxLength(100)]
    public string Descripcion { get; set; }
    [Required]
    public int CategoriaItemId { get; set; }
    [ForeignKey("CategoriaItemId")]
    public CategoriaItem CategoriaItem { get; set; }
    [Required]
    public decimal Precio { get; set; }
    [Required]
    public int NegocioId { get; set; }
    [ForeignKey("NegocioId")]
    public Negocio Negocio { get; set; }
}
