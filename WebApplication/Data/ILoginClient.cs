using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public interface ILoginClient
    {
        string BaseUrl { get; set; }

        Task<object> LoginAsync(UsuarioLogin usuarioLogin);
    }
}
