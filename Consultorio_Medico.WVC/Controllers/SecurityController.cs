using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Consultorio_Medico.BL;
using Consultorio_Medico.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Consultorio_Medico.MVC.Controllers
{
    public class SecurityController : Controller
    {
        private readonly ISecurityBL _securityBL;
        private readonly IRolBL _rolBL;
        public SecurityController(ISecurityBL securityBL, IRolBL rolBL)
        {
            _securityBL = securityBL;
            _rolBL = rolBL;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            //Esta linea sirve para cerrar sesion. 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = ReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string Login, string Password, string pReturnUrl = null)
        {
            try
            {
                var pUser = _securityBL.Login(Login, Password);

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
                    throw new Exception("Credenciales incorrectas");
                if (!string.IsNullOrWhiteSpace(pReturnUrl))
                    return Redirect(pReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {

                return RedirectToAction("AccesDenied", "Security");
            }
        }

        public IActionResult AccessDenied ()
        {
            return View();
        }

        // GET: SecurityController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SecurityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SecurityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SecurityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SecurityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SecurityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SecurityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
