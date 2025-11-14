namespace MVC.Interfaces;

public interface IUsuarioRepository
{
    // Retorna el objeto Usuario si las credenciales son válidas, sino null.
    Usuario GetUser(string username, string password);
}