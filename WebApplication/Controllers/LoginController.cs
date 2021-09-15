using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Servicio;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginClient _loginClient;
        private readonly IJwtTokenService _jwtTokenService;
        private string _tokenJWT;
        private UsuarioInfo _usuarioInfo;

        // OBTENEMOS MEDIANTE INYECCIÓN DE DEPENDENCIAS LA INSTANCIA
        // DE LoginClient y JwtTokenService A TRAVÉS DEL CONSTRUCTOR.
        public LoginController(ILoginClient loginClient,
                                            IJwtTokenService jwtTokenService)
        {
            _loginClient = loginClient;
            _jwtTokenService = jwtTokenService;
        }

        //SOBRESCRIBIMOS EL MÉTODO OnActionExecuting.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // OBTENEMOS EL TOKEN DE ACCESO DE LA COOKIE.
            _tokenJWT = context.HttpContext.Request.Cookies["_WebApiToken"];

            // DECODIFICAMOS EL TOKEN DE ACCESO, PARA OBTENER 
            // LOS DATOS DEL USUARIO AUTENTICADO.
            if (_tokenJWT != null)
            {
                _usuarioInfo = _jwtTokenService.DecodeJwtToken(_tokenJWT);
                // ALMACENAMOS LA INFORMACIÓN DEL USUARIO EN EL ViewData.
                ViewData["usuarioInfo"] = _usuarioInfo;
            }

            base.OnActionExecuting(context);
        }

        [HttpGet]
        public IActionResult Index(string error)
        {
            ViewData["error"] = error;
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioLogin usuarioLogin)
        {
            try
            {
                // OBTENEMOS EL OBJETO QUE CONTIENE EL TOKEN DE ACCESO DESDE
                // EL MÉTODO LoginAsync() DEL CLIENTE DE WEB API loginClient, 
                // PASÁNDOLE LAS CREDENCIALES DEL USUARIO usuarioLogin.            
                var loginResult = _loginClient.LoginAsync(usuarioLogin).Result.ToString();

                

                // CREAMOS UN OBJETO ANÓNIMO QUE REPRESENTE EL RESULTADO 
                // DEVUELTO POR EL MÉTODO LoginAsync() -> loginResult.
                var objetoAnonimo = new { nombre = string.Empty, token = string.Empty };

                // DESERIALIZAMOS loginResult SEGÚN EL OBJETO ANÓNIMO objetoAnonimo,
                // Y OBTENEMOS EL TOKEN JWT DE ACCESO.
                var tokenJWT = JsonConvert.DeserializeAnonymousType(loginResult, objetoAnonimo).token;

                //
               
                //

                

                // ALMACENAMOS EL TOKEN OBTENIDO EN UNA COOKIE.
                CookieOptions cookieOptions = new CookieOptions();
                // LE DAMOS 24 HORAS DE VIDA.
                cookieOptions.Expires = DateTime.Now.AddHours(24);
                cookieOptions.HttpOnly = true; // ??
                cookieOptions.Secure = true; // ??
                Response.Cookies.Append("FrontEnd", tokenJWT, cookieOptions);

                // REDIRIGIMOS A LA ACCIÓN Index DEL CONTROLADOR PaisController
                return RedirectToAction("Index", "Administracion");
            }
            catch (Exception ex)
            {
                // PARA CUALQUIER EXCEPCIÓN.
                ViewData["error"] = ex.Message;

                return View();
            }
        }
    }
}
