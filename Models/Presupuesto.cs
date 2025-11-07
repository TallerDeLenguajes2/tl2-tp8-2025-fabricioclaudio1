public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public string? NombreDestinario { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public List<PresupuestoDetalle> ListaDetalle { get; set; } = new List<PresupuestoDetalle>();
    // Producto  Cantidad
    
    public double MontoPresupuesto()
    {
        double montoPresupuesto = 0;
        foreach (var detalles in ListaDetalle)
        {
            montoPresupuesto += detalles.TipoProducto.Precio;
        }

        return montoPresupuesto;
    }
    
    public double MontoPresupuestoConIva()
    {
        double montoPresupuesto = MontoPresupuesto();
        montoPresupuesto *= 1.21;

        return montoPresupuesto;
    }

    public int CantidadProductos()
    {
        int cantidadProductos = 0;
        foreach (var detalles in ListaDetalle)
        {
            cantidadProductos += detalles.Cantidad;
        }

        return cantidadProductos;
    }
}
