using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Servicio
{
    public interface IJwtTokenService
    {
        UsuarioInfo DecodeJwtToken(string token);
    }
}
