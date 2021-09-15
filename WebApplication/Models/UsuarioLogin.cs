using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class UsuarioLogin
    {
        [JsonProperty("usuario", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Usuario { get; set; }

        [JsonProperty("password", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
    }
}
