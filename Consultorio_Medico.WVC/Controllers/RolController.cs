using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class RolController : Controller
    {
        private readonly IRolBL _rolBL;
        private readonly ILogger<RolController> _logger;
        public RolController(IRolBL RolBL, ILogger<RolController> logger)
        {
            _rolBL = RolBL;
            _logger = logger;
        }
       
        // GET: RolController
        public async Task<ActionResult> Index(RolSearchingInputDTO rol)
        {
            _logger.LogInformation("-------- INICIO METODO INDEX ROL CONTROLLER -----------");
            var list = await _rolBL.Search(rol);
            _logger.LogInformation("------ FIN METODO INDEX ROL CONTROLLER --------");
            return View(list);  
        }

        // GET: RolController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("--------- INICIO METODO DETAILS ROL CONTROLLER -------");
            RolSearchingOutputDTO rol = await _rolBL.GetById(Id);
            _logger.LogInformation("-------- FIN METODO INDEX ROL CONTROLLER -----------");
            return View(rol);


        }

        // GET: RolController/Create
        public ActionResult Create()
        {

            ViewBag.ErrorMessage = "";
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        
        public async Task<ActionResult> Create(RolInputDTO pRol)
        {
            try
            {
                _logger.LogInformation("------ INICIO METODO CREATE POST ROL CONTROLLER -------");
                int result = await _rolBL.Create(pRol);
                if (result > 0) {
                    _logger.LogInformation("----- REGISTRO CREADO : CREATE POST ROL CONTROLLER -----");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("--- NO SE PUDO CREAR EL REGISTRO : CREATE POST ROL CONTROLLER -----");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pRol);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("---- ERROR : " + ex.Message + " ----------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RolController/Edit/5
        public async Task<ActionResult>Edit(int id)
        {
            _logger.LogInformation("--- INICIO METODO EDIT GET ROL CONTROLLER -------");
            RolSearchingOutputDTO rol = await _rolBL.GetById(id);
            var rolResults = new RolInputDTO()
            {
                RolId = rol.RolId,
                Name = rol.Name,
                Status = rol.Status
            };
            _logger.LogInformation("----- FIN METODO EDIT GET ROL CONTROLLER ----");
            return View(rolResults);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, RolInputDTO pRol)
        {
            try
            {
                _logger.LogInformation("------- INICIO METODO EDIT POST ROL CONTROLLER ----------");
                int result = await _rolBL.Update(pRol);
                if (result > 0) {
                    _logger.LogInformation("------ FIN METODO EDIT POST ROL CONTROLLER -------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("--- NO SU PUDO EDITAR : EDIT POST ROL CONTROLLER");
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(pRol);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("----- ERROR: " + ex.Message + " ---------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: RolController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"-- INICIO METODO DELETE GET ROL CONTROLLER ---");
            RolSearchingOutputDTO rol = await _rolBL.GetById(id);
            _logger.LogInformation("----- FIN METODO DELETE GET ROL CONTROLLER ---");
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
 
        public async Task<ActionResult> Delete(int Id, RolSearchingOutputDTO pRol)
        {
            try
            {
                _logger.LogInformation("------- INICIO METODO DELETE POST ROL CONTROLLER --------");
                int result = await _rolBL.Delete(Id);
                if (result > 0)
                {
                    _logger.LogInformation("------ FIN METODO DELETE POST ROL CONTROLLER ");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("--- NO SE ELIMINO EL REGISTRO : DELETE POST ROL CONTROLLER -----");
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("---- ERROR : " + ex.Message + " ------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
