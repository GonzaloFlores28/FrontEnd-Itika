using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Servicio
{
    public class JwtTokenService : IJwtTokenService
    {
        public UsuarioInfo DecodeJwtToken(string token)
        {
            // EXTRAE LA INFORMACIÓN DEL TOKEN JWT.
            var handler = new JwtSecurityTokenHandler();
            var _token = handler?.ReadJwtToken(token);

            // CREA EL PERFIL DE INFORMACIÓN DEL USUARIO
            // A PARTIR DE LOS CLAIMS DEL TOKEN JWT
            var _usuarioInfo = new UsuarioInfo()
            {
                Id = new Guid(_token?.Claims?.
                    SingleOrDefault(x => x.Type == "nameid")?.Value
                    ?? _token.Id),

                Nombre = _token?.Claims?.
                    SingleOrDefault(x => x.Type == "nombre")?.Value,

                Apellidos = _token?.Claims?.
                    SingleOrDefault(x => x.Type == "apellidos")?.Value,

                Email = _token?.Claims?.
                    SingleOrDefault(x => x.Type == "email")?.Value,

                Rol = _token?.Claims?.
                    SingleOrDefault(x => x.Type.Contains("role"))?.Value,

                ValidoDesde = _token.ValidFrom,

                ValidoHasta = _token.ValidTo
            };
            return _usuarioInfo;
        }
    }
}
