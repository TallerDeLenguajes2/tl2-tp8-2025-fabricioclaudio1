using System.ComponentModel.DataAnnotations;

public class PresupuestoViewModel
{
    public int IdPresupuesto { get; set; }
    public string? NombreDestinario { get; set; }
    [EmailAddress]
    public string? CorreoElectronico { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime FechaCreacion { get; set; }

    public List<PresupuestoDetalle> ListaDetalle { get; set; } = new List<PresupuestoDetalle>();

}
