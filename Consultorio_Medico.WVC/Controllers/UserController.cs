﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.DTOs;
using Consultorio_Medico.Entities.Interfaces;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Serilog;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class UserController : Controller
    {
        private readonly IUserBL _UserBL;
        private readonly IRolBL _rolBL;
        private readonly ISpecialtieBL _specialtieBL;
        private readonly IScheduleBL _scheduleBL;
        private readonly IWorkPlaceBL _workPlaceBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL UserBL, IRolBL rolBL, IWorkPlaceBL workPlaceBL, ISpecialtieBL specialtieBL, IScheduleBL scheduleBL, ILogger<UserController> logger)
        {
            _UserBL = UserBL;
            _rolBL = rolBL; 
            _workPlaceBL = workPlaceBL;
            _specialtieBL = specialtieBL;
            _scheduleBL = scheduleBL;
            _logger = logger;
        }

        // GET: UserController
        public async Task<IActionResult> Index(userSearchInputDTO user)
        {
            _logger.LogInformation("--- INICIO METODO INDEX USER CONTROLLER ---");
            var list = await _UserBL.Search(user);
            _logger.LogInformation("--- FIN METODO INDEX USER CONTROLELR ---");
            return View(list);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("---- INICIO METODO DETAILS USER CONTROLLER ----");
            RolSearchingInputDTO rol = new RolSearchingInputDTO();
            WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
            ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();

            var specialtiesList = await _specialtieBL.Search(specialtie);
            var scheduleList = await _scheduleBL.Search(schedule);
            var Rollist = await _rolBL.Search(rol);
            var worklist = await _workPlaceBL.Search(workpalce);
            var user = await _UserBL.GetById(Id);

            ViewBag.ErrorMessage = "";
            ViewBag.Specialties = specialtiesList;
            ViewBag.Schedules = scheduleList;
            ViewBag.Roles = Rollist;
            ViewBag.Workplace = worklist;
            _logger.LogInformation("---- FIN METODO DETAILS USER CONTROLLER ----");
            return View(user);
        }

        // GET: UserController/Create
        public async Task<ActionResult> Create() //:3
        {
            RolSearchingInputDTO rol = new RolSearchingInputDTO();
            WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
            ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


            var specialtiesList = await _specialtieBL.Search(specialtie);
            var scheduleList = await _scheduleBL.Search(schedule);
            var Rollist = await _rolBL.Search(rol);
            var worklist = await _workPlaceBL.Search(workpalce);

            ViewBag.ErrorMessage = "";
            ViewBag.Specialties = specialtiesList;
            ViewBag.Schedules = scheduleList;
            ViewBag.Roles = Rollist;
            ViewBag.Workplace = worklist;
            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserAddDTO pUser)
        {
            try
            {
                _logger.LogInformation("--- INICIO CREATE POST USER CONTROLLER ----");
                int result = await _UserBL.Create(pUser);
                if (result > 0)
                {
                    _logger.LogInformation("---- FIN CREATE POST USER CONTROLLER ---");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("---- NO SE CREO EL REGISTRO : CREATE POST USER CONTROLLER ----");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pUser);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("---- ERRROR : " + ex.Message + " ----");
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();

                var Rollist = await _rolBL.Search(rol);
                var worklist = await _workPlaceBL.Search(workpalce);

                ViewBag.ErrorMessage = "";
                ViewBag.Roles = Rollist;
                ViewBag.Workplace = worklist;
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        //GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("---- INICIO METODO EDIT GET USER CONTROLLER ----");
            RolSearchingInputDTO rol = new RolSearchingInputDTO();
            WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
            ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


            var specialtiesList = await _specialtieBL.Search(specialtie);
            var scheduleList = await _scheduleBL.Search(schedule);
            var Rollist = await _rolBL.Search(rol);
            var worklist = await _workPlaceBL.Search(workpalce);

            ViewBag.ErrorMessage = "";
            ViewBag.Specialties = specialtiesList;
            ViewBag.Schedules = scheduleList;
            ViewBag.Roles = Rollist;
            ViewBag.Workplace = worklist;

            var Users = await _UserBL.GetById(Id);
            var UserResults = new userUpdateDTO()
            {
                
                UserId = Users.UserId,
                RolId = Users.RolId,
                WorkplacesId = Users.WorkPlacesId,
                Name = Users.Name,  
                LastName = Users.LastName,
                PhoneNumber = Users.PhoneNumber,
                Dui = Users.Dui,
                Email = Users.Email,
                Login = Users.Login,
                Status =Users.Status,

            };
            _logger.LogInformation("---- FIN METODO EDIT GET USER CONTROLLER ----");
            return View(UserResults);
        }

        // POST: UserController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int Id, userUpdateDTO User)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO EDIT POST USER CONTROLLER ----");
                int result = await _UserBL.Update(User);
                if (result > 0)
                {
                    _logger.LogInformation("---- FIN METODO EDIT POST USER CONTROLLER ---");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("---- ERROR AL EDITAR :  EDIT POST USER CONTROLLER ----");
                    ViewBag.ErrorMessage = "Error no se pudo modificar";
                    return View(User);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
                ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
                SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


                var specialtiesList = await _specialtieBL.Search(specialtie);
                var scheduleList = await _scheduleBL.Search(schedule);
                var Rollist = await _rolBL.Search(rol);
                var worklist = await _workPlaceBL.Search(workpalce);

                ViewBag.ErrorMessage = "";
                ViewBag.Specialties = specialtiesList;
                ViewBag.Schedules = scheduleList;
                ViewBag.Roles = Rollist;
                ViewBag.Workplace = worklist;

                ViewBag.ErrorMessage = ex.Message;
                return View(User);
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation("---- INICIO METODO DELETE GET USER CONTROLLER ----");
            RolSearchingInputDTO rol = new RolSearchingInputDTO();
            WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
            ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


            var specialtiesList = await _specialtieBL.Search(specialtie);
            var scheduleList = await _scheduleBL.Search(schedule);
            var Rollist = await _rolBL.Search(rol);
            var worklist = await _workPlaceBL.Search(workpalce);

            ViewBag.ErrorMessage = "";
            ViewBag.Specialties = specialtiesList;
            ViewBag.Schedules = scheduleList;
            ViewBag.Roles = Rollist;
            ViewBag.Workplace = worklist;

            var User = await _UserBL.GetById(Id);
            _logger.LogInformation("---- FIN METODO DELETE GET USER CONTROLLER ----");
            return View(User);
        }

        //  UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, userSearchOutputDTO pUser)
        {
            try
            {
                _logger.LogInformation("--- INICIO METODO DELETE POST USER CONTROLLER ----");
                int result = await _UserBL.Delete(Id);
                if (result > 0)
                {
                    _logger.LogInformation("---- FIN METODO DELETE POST USER CONTROLLER ----");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("NO SE PUDO ELIMINAR : DELETE POST USER CONTROLLER ----");
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(pUser);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR: " + ex.Message + " ----");
                RolSearchingInputDTO rol = new RolSearchingInputDTO();
                WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
                ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
                SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


                var specialtiesList = await _specialtieBL.Search(specialtie);
                var scheduleList = await _scheduleBL.Search(schedule);
                var Rollist = await _rolBL.Search(rol);
                var worklist = await _workPlaceBL.Search(workpalce);

                ViewBag.ErrorMessage = "";
                ViewBag.Specialties = specialtiesList;
                ViewBag.Schedules = scheduleList;
                ViewBag.Roles = Rollist;
                ViewBag.Workplace = worklist;

                ViewBag.ErrorMessage = ex.Message;
                return View(pUser);
            }
        }
    }
}
