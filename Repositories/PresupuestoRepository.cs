
using Microsoft.Data.Sqlite;
public class PresupuestoRepository
{
    string cadenaConexion = "Data Source=Tienda.db";

    public bool Crear(Presupuesto presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "INSERT INTO Presupuesto (NombreDestinario) VALUES(@nombreDestinario)";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@nombreDestinario", presupuesto.NombreDestinatario));
        comando.ExecuteNonQuery();

        return true;
    }
    public List<Presupuesto> Listar()
    {
        List<Presupuesto> presupuestos = new();
        string queryString = "SELECT * FROM Productos";
        using (SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read()) // si encontró un registro
                {
                    var presupuesto = new Presupuesto
                    {
                        IdPresupuesto = reader.GetInt32(0),
                        NombreDestinatario = reader.GetString(1),
                    };
                    presupuestos.Add(presupuesto);
                }
                connection.Close();
            }

            return presupuestos;
        }
    }
    public Presupuesto? ObtenerSegunID(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = @"
        SELECT 
            pres.NombreDestinario, 
            pd.Cantidad, 
            prod.IdProducto, prod.Descripcion, prod.PrecioNumerico
        FROM Presupuesto pres
        INNER JOIN PresupuestosDetalle pd ON pres.IdPresupuesto = pd.IdPresupuesto
        INNER JOIN Productos prod ON prod.IdProducto = pd.IdProducto
        WHERE pres.IdPresupuesto = @Id
        ";

        //pre.IdPresupuesto
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        using var reader = comando.ExecuteReader();

        Presupuesto? presupuesto = null;
        PresupuestoDetalle? pd = null;
        Producto? prod = null;

        while (reader.Read()) // si encontró un registro
        {
            presupuesto ??= new Presupuesto
            {
                NombreDestinatario = reader.GetString(0)
            };

            prod = new Producto
            {
                IdProducto = reader.GetInt32(2),
                Descripcion = reader.GetString(3),
                Precio = reader.GetDouble(4)
            };

            pd = new PresupuestoDetalle
            {
                TipoProducto = prod,
                Cantidad = reader.GetInt32(1)
            };

            presupuesto.ListaDetalle.Add(pd);
        }

        return presupuesto;
    }
    public bool Modificar(int id, Presupuesto presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "UPDATE Productos SET NombreDestinario = @NombreDestinario WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@NombreDestinario", presupuesto.NombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        comando.ExecuteNonQuery();

        return true;
    }
    public bool EliminarID(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        string sql = "DELETE FROM Presupuesto WHERE Id = @Id";
        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Id", id));
        comando.ExecuteNonQuery();

        return true;
    }

}