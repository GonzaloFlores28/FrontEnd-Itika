using FrontEnd.Models;

namespace FrontEnd.Servicio
{
    public interface IJwtTokenService
    {
        UsuarioInfo DecodeJwtToken(string token);
    }
}