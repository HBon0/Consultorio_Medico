using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.DTOs.UserSchedule;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class UserScheduleController : Controller
    {
        private readonly IUserSchedulesBL _userSchedBL;
        private readonly IUserBL _UserBL;
        private readonly IScheduleBL _scheduleBL;
        private readonly ILogger<UserScheduleController> _logger;

        public UserScheduleController(IUserSchedulesBL UserChedBL, IUserBL UserBL, IScheduleBL scheduleBL, ILogger<UserScheduleController> logger)
           {
            _UserBL = UserBL;
            _userSchedBL = UserChedBL;
            _scheduleBL = scheduleBL;
            _logger = logger;
        }
        // GET: UserScheduleController
        public async Task<ActionResult> Index(UserScheduleSearchInpuntDTO UserChed) 
        {
            _logger.LogInformation("---- INICIO METODO INDEX USER SCHEDULE CONTROLLER ----");
            var list = await _userSchedBL.Search(UserChed);
            _logger.LogInformation("---- FIN METODO INDEX USER SCHEDULE CONTROLLER -----");
            return View(list);
        }

        // GET: UserScheduleController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("---- INICIO METODO DETAILS USER SCHEDULE CONTROLLER -----");
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var userSchedule = await _userSchedBL.GetById(Id);
            _logger.LogInformation("---- FIN METODO DETAILS USER SCHEDULE CONTROLLER -----");
            return View(userSchedule);
        }

        // GET: UserScheduleController/Create
        public async Task<ActionResult> Create()
        {
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;
            return View();
        }

        // POST: UserScheduleController/Create
        [HttpPost]  
        public async Task<ActionResult> Create(UserScheduleInputDTO pUserSched)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO CREATE POST USER SCHEDULE CONTROLLER ----");
                int result = await _userSchedBL.Create(pUserSched);
                if (result > 0)
                {
                    _logger.LogInformation("--- FIN METODO CREATE POST USER SCHEDULE CONTROLLER ----");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("---- NO SE PUDO CREAR : CREATE POST USER SCHEDULE CONTROLLER ----");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pUserSched);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("---- ERROR : " + ex.Message + " -----");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: UserScheduleController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("---- INICIO METODO EDIT GET USER SCHEDULE CONTROLLER ----");
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var UserSchedule = await _userSchedBL.GetById(Id);
            _logger.LogInformation("---- FIN METODO EDIT GET USER SCHEDULE CONTROLLER ----");
            return View(new UserScheduleInputDTO
            {
                UserScheduleId = UserSchedule.UserSchedulesId,
                UserId = UserSchedule.UserId,
                SchedulesId = UserSchedule.SchedulesId
            });
        }

        // POST: UserScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, UserScheduleInputDTO pUserSchedule)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO EDIT POST USER SCHEDULE CONTROLLER ----");
                if (!ModelState.IsValid)
                    return View(pUserSchedule);
                int result = await _userSchedBL.Update(pUserSchedule);

                if (result > 0)
                {
                    _logger.LogInformation("---- FIN METODO EDIT POST USER SCHEDULE CONTROLLER ----");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("---- NO SE EDITO : EDIT POST USER SCHEDULE CONTROLLER ----");
                    ViewBag.ErrorMessage = "Error al Modificar registro. ";
                    return View(pUserSchedule);
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("---- ERROR : " + ex.Message + " ----");
                return View();
            }
        }

        // GET: UserScheduleController/Delete/5
        public async  Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation("---- INICIO METODO DELETE GET USER SCHEDULE CONTROLLER ----");
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var UserSchedule = await _userSchedBL.GetById(Id);
            _logger.LogInformation("---- FIN METODO DELETE GET USER SCHEDULE CONTROLLER ----");
            return View(new UserScheduleSearchOutputDTO
            {
                UserSchedulesId = UserSchedule.UserSchedulesId,
                UserId = UserSchedule.UserId,
                SchedulesId = UserSchedule.SchedulesId
            });
        }

        // POST: UserScheduleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, userSearchOutputDTO pUserSchedule)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO DELETE POST USER SCHEDULE CONTROLLER ----");
                if (!ModelState.IsValid)
                    return View(pUserSchedule);
                int result = await _userSchedBL.Delete(Id);

                if (result > 0)
                {
                    _logger.LogInformation("---- FIN METODO DELETE POST USER SCHEDULE CONTROLLER ----");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("---- NO SE ELIMNO : DELETE POST USER SCHEDULE CONTROLLER ---");
                    ViewBag.ErrorMessage = "Error al Eliminar registro. ";
                    return View(pUserSchedule);
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                return View(pUserSchedule);
            }
        }
    }
}
