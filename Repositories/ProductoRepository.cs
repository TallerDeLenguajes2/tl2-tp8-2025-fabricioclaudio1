using System.ComponentModel;
using Microsoft.Data.Sqlite;
public class ProductoRepository : IProductoRepository
{
    readonly string cadenaConexion = "Data Source=Tienda.db";

    public bool Crear(Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "INSERT INTO Productos (Descripcion, PrecioNumerico) VALUES(@descripcion, @precio)";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        comando.ExecuteNonQuery();

        return true;
    }
    public List<Producto> Listar()
    {
        List<Producto> productos = new List<Producto>();
        string queryString = "SELECT IdProducto, Descripcion, PrecioNumerico FROM Productos";
        using SqliteConnection connection = new SqliteConnection(cadenaConexion);
        SqliteCommand command = new SqliteCommand(queryString, connection);

        connection.Open();
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read()) // si encontró un registro
        {
            var producto = new Producto
            {
                IdProducto = reader.GetInt32(0),
                Descripcion = reader.GetString(1),
                Precio = reader.GetInt32(2),
            };
            productos.Add(producto);
        }
        connection.Close();


        return productos;
    }
    public Producto? ObtenerID(int id)
    {
        string sql = "SELECT IdProducto, Descripcion, PrecioNumerico FROM Productos WHERE IdProducto = @Id";
        using SqliteConnection conexion = new SqliteConnection(cadenaConexion);
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));

        conexion.Open();
        using var reader = comando.ExecuteReader();
        if (reader.Read()) // si encontró un registro
        {
            var producto = new Producto
            {
                IdProducto = reader.GetInt32(0),
                Descripcion = reader.GetString(1),
                Precio = reader.GetInt32(2),
            };
            return producto;
        }
        conexion.Close();

        return null;
    }
    public bool Modificar(int id, Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        string sql = "UPDATE Productos SET Descripcion = @Descripcion, PrecioNumerico = @Precio WHERE IdProducto = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
        comando.Parameters.Add(new SqliteParameter("@Id", id));

        conexion.Open();
        comando.ExecuteNonQuery();
        conexion.Close();

        return true;
    }
    public bool EliminarID(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        
        string sql = "DELETE FROM Productos WHERE IdProducto = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));

        conexion.Open();
        comando.ExecuteNonQuery();
        conexion.Close();
        
        return true;
    }

}