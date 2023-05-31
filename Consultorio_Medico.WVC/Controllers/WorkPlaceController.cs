using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.BL.DTOs.userDTO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class WorkPlaceController : Controller
    {

        private readonly IWorkPlaceBL workPlaceBL;
        private readonly ILogger<WorkPlaceController> _logger;
        public WorkPlaceController(IWorkPlaceBL workplaceBL, ILogger<WorkPlaceController> logger)
        {
            workPlaceBL = workplaceBL;
            _logger = logger;
        }
        // GET: WorkPlaceController
        public async Task<ActionResult>  Index (WokplaceSearchInputDTO wokplace)
        {
            _logger.LogInformation("--------------- INICIO DE METODO INDEX WORKPLACE CONTROLLER -----------------------");
            var list = await workPlaceBL.Search(wokplace);
            _logger.LogInformation("----------------- FIN METODO INDEX WORKPLACE CONTROLLER ---------------------------");
            return View(list);
        }

        // GET: WorkPlaceController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("----------------- INICIO DE METODO DETAILS WORKPLACE CONTROLLER -------------------------");
            WorkPlaceSearchOutPutDTO workplace = await workPlaceBL.GetById(Id);
            _logger.LogInformation("----------------- FIN DE METODO DETAILS WORKPLACE CONTROLLER ---------------------------");
            return View(workplace);
        }
        

        // GET: WorkPlaceController/Create
        public ActionResult Create()
        {

            ViewBag.ErrorMessage = "";
            return View();
        }

        // POST: WorkPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkPlaceInputDTO pWork)
        {
            try
            {
                _logger.LogInformation("-------------------------- INCIO DE METODO CREATE WORKPLACE CONTROLLER -----------------------");
                int result = await workPlaceBL.Create(pWork);
                if (result > 0)
                {
                    _logger.LogInformation("-------------------- FIN METODO CREATE WORKPLACE CONTROLLER -------------------------");
                    return RedirectToAction(nameof(Index));
                }
                    
                else
                {
                    _logger.LogError("----------------------- ERROR AL GUARDAR REGISTRO EN CREATE WORKPLACE CONTROLLER -----------------------");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pWork);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("-------------------------- ERROR EN METODO CREATE WORKPLACE CONTROLLER : " + ex.Message + " ------------------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: WorkPlaceController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("----------------- INICO METODO EDIT GET WORKPLACE CONTROLLER ------------------------------------");
            WorkPlaceSearchOutPutDTO Workplaces = await workPlaceBL.GetById(Id);
            var UserResults = new WorkPlaceInputDTO()
            {

                WorkPlacesId = Workplaces.WorkPlacesId,
                WorkPlaces = Workplaces.WorkPlaces,
       
            };
            _logger.LogInformation("-------------------- FIN METODO EDIT GET WORKPLACE CONTROLLER -----------------------------");
            return View(UserResults);
        }

        // POST: WorkPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, WorkPlaceInputDTO Workplace)
        {
            try
            {
                _logger.LogInformation("---------------------- INICIO METODO EDIT POST WORKPLACE CONTROLLER ------------------------");
                int result = await workPlaceBL.Update(Workplace);
                if (result > 0)
                {
                    _logger.LogInformation("---------------- FIN METODO EDIT POST WORKPLACE CONTROLLER -------------------------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("------------------ ERROR AL EDITAR REGISTRO EN WORKPLACE CONTROLLER -------------------------");
                    ViewBag.ErrorMessage = "Error no pudo modificar";
                    return View(Workplace) ;
                }
            }
            catch  (Exception ex)
            {
                _logger.LogError("------------------ ERROR EN METODO TRY : " + ex.Message + " -----------------------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        
        // GET: WorkPlaceController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation("------------------------- INICIO METODO DELETE GET WORKPLACE CONTROLELR -------------------------");
            WorkPlaceSearchOutPutDTO Worlplace = await workPlaceBL.GetById(Id);
            _logger.LogInformation("---------------------------- FIN METODO DELETE GET WORKPLACE CONTROLLER ------------------------");
            return View(Worlplace);
        }
    
        // POST: WorkPlaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, WorkPlaceSearchOutPutDTO Workplace)
        {
            try
            {
                _logger.LogInformation("------------------ INICIO METODO DELETE POST WORKPLACE CONTROLLER --------------------------");
                int result = await workPlaceBL.Delete(Id);
                if (result > 0) {
                    _logger.LogInformation("---------------- FIN METODO DELETE POST WORKPLACE CONTROLLER -----------------------------");
                    return RedirectToAction(nameof(Index));
                }
                else {
                    _logger.LogInformation("----------------- ERROR AL TRATAR DE ELIMINAR EL REGISTRO WORKPLACE CONTROLLER ------------------");
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                        return View(Workplace);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("----------------------- ERROR : " + ex.Message + "-------------------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}

