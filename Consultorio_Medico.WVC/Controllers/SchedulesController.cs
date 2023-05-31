using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class SchedulesController : Controller
    {
        private readonly IScheduleBL _scheduleBL;
        private readonly ILogger<SchedulesController> _logger;

        public SchedulesController(IScheduleBL scheduleBL, ILogger<SchedulesController> logger)
        {
            _scheduleBL = scheduleBL;
            _logger = logger;
        }

        
        // GET: SchedulesController
        public async Task<ActionResult> Index(ScheduleSearchInputDTO schedules)
        {

            _logger.LogInformation("---------------- INICIO METODO INDEX SCHEDULE CONTROLLER --------------------------");
            var list = await _scheduleBL.Search(schedules);
            _logger.LogInformation("---------------- FIN METODO INDEX SCHEDULE CONTROLLER --------------------------");
            return View(list);
        }

        // GET: SchedulesController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("-------------------- INICIO METODO DETAILS SCHEDULE CONTROLLER ------------------------");
            var Schedules = await _scheduleBL.GetById(Id);
            _logger.LogInformation("---------------------- FIN METODO DETAILS SCHEDULE CONTROLLER --------------------------");
            return View(Schedules);
        }

        // GET: SchedulesController/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        // POST: SchedulesController/Create
        [HttpPost]
        public async Task<ActionResult> Create(ScheduleInputDTO schedule)
        {

            try
            {
                _logger.LogInformation("---------------------- INICIO METODO CREATE POST DE SCHEDULE CONTROLLER ------------------------------");
                int result = await _scheduleBL.Create(schedule);
                if (result > 0)
                {
                    _logger.LogInformation("---------------------- FIN METODO CREATE POST DE SCHEDULE CONTROLLER ----------------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("--------------------- NO SE PUDO CREAR EL REGISTRO: CREATE POST DE SCHEDULE CONTROLLER ------------------");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(schedule);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation("--------- ERROR : " + ex.Message + " ------------------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SchedulesController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("------------ INICIO METODO EDIT GET SCHEDULE CONTROLLER ---------------------");
            ScheduleSearchOutPutDTO schedule = await _scheduleBL.GetById(Id);
            var SchedResult = new ScheduleInputDTO()
            {
                SchedulesId = schedule.SchedulesId,
                DayName = schedule.DayName,
                StartOfShift = schedule.StartOfShift,   
                EndOfShift=schedule.EndOfShift,

            };
            _logger.LogInformation(" --------------- FIN METODO EDIT GET SCHEDULE CONTROLLER ----------------------------");
            return View(SchedResult);
        }

        // POST: SchedulesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ScheduleInputDTO schedule)
        {
            try
            {
                _logger.LogInformation("--------------- INICIO METODO EDIT POST SCHEDULE CONTROLLER ------------------------");
                int result = await _scheduleBL.Update(schedule);
                if (result > 0) {
                    _logger.LogInformation("------------ FIN METODO EDIT POST SCHEDULE CONTROLLER: REGISTRO MODIFICADO ------------- ");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("------------- ERROR AL EDITAR REGISTRO : EDIT POST SCHEDULE CONTROLLER --------------------");
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(schedule);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("------ ERROR : " + ex.Message + " ---------------------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SchedulesController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation("-------------- INICIO DE METODO DELETE GET SCHEDULE CONTROLLER ------------------------");
            ScheduleSearchOutPutDTO customer = await _scheduleBL.GetById(Id);
            _logger.LogInformation("------------- FIN DE METODO DELETE GET SCHEDULE CONTROLLER ----------------------------");
            return View(customer);
        }

        // POST: SchedulesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Delete(int Id, ScheduleSearchOutPutDTO schedule)
        {
            try
            {
                _logger.LogInformation("-------------- INICIO DE METODO DELETE POST SCHEDULE CONTROLELR -----------------------");
                int result = await _scheduleBL.Delete(Id);
                if (result > 0) {
                    _logger.LogInformation("---------- REGISTRO ELIMINADO CORRECTAMENTE : DELETE POST SCHEDULE CONTROLLER ------------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("---------------- ERROR AL ELIMINAR REGISTRO : DELETE POST SCHEDULE CONTROLLER ----------------------");
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(schedule);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                return View();

            }
  
    }
  }
}
