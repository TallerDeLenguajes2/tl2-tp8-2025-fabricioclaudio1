public interface IProductoRepository
{
    public bool Crear(Producto producto);
    public List<Producto> Listar();
    public Producto? ObtenerID(int id);
    public bool Modificar(int id, Producto producto);
    public bool EliminarID(int id);
}