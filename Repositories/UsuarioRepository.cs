using Microsoft.Data.Sqlite;
using MVC.Interfaces;

namespace MVC.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
    readonly string cadenaConexion = "Data Source=Tienda.db";

    // Lógica para conectar con la DB y buscar por user/pass.
    public Usuario GetUser(string usuario, string contrasena)
    {
        Usuario? user = null;
        //Consulta SQL que busca por Usuario Y Contrasena
        const string sql = @" SELECT Id, Nombre, User, Pass, Rol FROM Usuarios
            WHERE User = @Usuario AND Pass = @Contrasena";
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        using var comando = new SqliteCommand(sql, conexion);

        // Se usan parámetros para prevenir inyeccion SQL

        comando.Parameters.AddWithValue("@Usuario", usuario);
        comando.Parameters.AddWithValue("@Contrasena", contrasena);
        using var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            // Si el lector encuentra una fila, el usuario existe y las credenciales son correctas
            user = new Usuario
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                User = reader.GetString(2),
                Pass = reader.GetString(3),
                Rol = reader.GetString(4)
            };
        }
        return user;
    }
}
