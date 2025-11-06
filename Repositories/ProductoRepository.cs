using System.ComponentModel;
using Microsoft.Data.Sqlite;
public class ProductoRepository : IProductoRepository
{
    string cadenaConexion = "Data Source=Tienda.db";

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
        string queryString = "SELECT Descripcion, PrecioNumerico FROM Productos";
        
        using SqliteConnection connection = new SqliteConnection(cadenaConexion);
        SqliteCommand command = new SqliteCommand(queryString, connection);
        connection.Open();
        using (SqliteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read()) // si encontró un registro
            {
                var producto = new Producto
                {
                    Descripcion = reader.GetString(0),
                    Precio = reader.GetInt32(1),
                };
                productos.Add(producto);
            }
            connection.Close();
        }

        return productos;
    }
    public Producto ObtenerID(int id)
    {
        string sql = "SELECT nombre, dni, telefono FROM Paciente WHERE Id = @Id";
        
        using SqliteConnection conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        using var reader = comando.ExecuteReader();
        if (reader.Read()) // si encontró un registro
        {
            var paciente = new Producto
            {
                IdProducto = reader.GetInt32(0),
                Descripcion = reader.GetString(1),
                Precio = reader.GetInt32(2),
            };
            return paciente;
        }
        return null;
    }
    public bool Modificar(int id, Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "UPDATE Producto SET Precio = @Precio WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        comando.ExecuteNonQuery();

        return true;
    }
    public bool EliminarID(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "DELETE FROM Producto WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        comando.ExecuteNonQuery();

        return true;
    }

}