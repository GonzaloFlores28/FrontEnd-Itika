using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Data
{
    public interface ILoginClient
    {
        string BaseUrl { get; set; }

        Task<object> LoginAsync(UsuarioLogin usuarioLogin);
    }
}
