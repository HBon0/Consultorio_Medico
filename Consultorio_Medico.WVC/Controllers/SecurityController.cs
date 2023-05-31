using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Consultorio_Medico.BL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Consultorio_Medico.MVC.Controllers
{
    public class SecurityController : Controller
    {
        private readonly ISecurityBL _securityBL;
        private readonly IRolBL _rolBL;
        private readonly ILogger<SecurityController> _logger;
        public SecurityController(ISecurityBL securityBL, IRolBL rolBL, ILogger<SecurityController> logger)
        {
            _securityBL = securityBL;
            _rolBL = rolBL;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            _logger.LogInformation("---------- INICIO METODO LOGIN GET SECURITY CONTROLLER -------------");
            //Esta linea sirve para cerrar sesion. 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = ReturnUrl;
            ViewBag.Error = "";
            _logger.LogInformation("------- FIN METODO LOGIN GET SECURITY CONTROLLER -----------");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string Login, string Password, string pReturnUrl = null)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO LOGIN POST SECURITY CONTROLLER ---------");
                var pUser = await _securityBL.Login(Login, Password);

                //Condiciones que debe cumplir el usuario para tener credenciales correctas.
                if (pUser != null)
                {
                    var rol = await _rolBL.GetById(pUser.rolId);
                    pUser.RolName = rol.Name;
                    var claims = new[] { new Claim(ClaimTypes.Name, pUser.Login), new Claim(ClaimTypes.Role, pUser.RolName) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                {
                    _logger.LogInformation($"---- LOGEO FALLIDO : LOGIN POST SECURITY CONTROLLER --------");
                    throw new Exception("Credenciales incorrectas");
                }
                if (!string.IsNullOrWhiteSpace(pReturnUrl))
                {

                    return Redirect(pReturnUrl);
                }
                else
                {
                    _logger.LogInformation("------ FIN METODO LOGIN POST SECURITY CONTROLLER -----------");
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("---- ERROR : " + e.Message + " ------------");
                return RedirectToAction("AccesDenied", "Security");
            }
        }

        public IActionResult AccessDenied ()
        {
            return View();
        }

        
    }
}
