using System.ComponentModel.DataAnnotations;

public class ProductoViewModel
{
    public int IdProducto { get; set; }

    [Required]
    [StringLength(250)]
    public string? Descripcion { get; set; }

    [Required]
    [Range(0.01, 999999)]
    public double Precio { get; set; }

}