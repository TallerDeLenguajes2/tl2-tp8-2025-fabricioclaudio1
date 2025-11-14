using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using MVC.Repositories;

namespace MVC.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly UsuarioRepository _repoUsuario;

    public AuthenticationService()
    {
        _repoUsuario = new();
    }

    public bool Login(string username, string password)
    {
        Usuario usu = _repoUsuario.GetUser(username, password);

        if (usu != null)
        {
            return true;
        }

        return false;
    }
    public void Logout()
    {
        throw new NotImplementedException();
    }
    public bool IsAuthenticated()
    {
        throw new NotImplementedException();
    }

    // Verifica si el usuario actual tiene el rol requerido (ej. "Administrador").
    public bool HasAccessLevel(string requiredAccessLevel)
    {
        //Usuario usu = _repoUsuario.GetUser(username, password);
        if (requiredAccessLevel != "Aqui se utiliza una cookie supuestamente")
        {
            return true;
        }
        return false;
    }


}