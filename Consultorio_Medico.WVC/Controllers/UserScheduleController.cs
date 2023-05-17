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
        readonly IUserSchedulesBL _userSchedBL;
        readonly IUserBL _UserBL;
        readonly IScheduleBL _scheduleBL;
        public UserScheduleController(IUserSchedulesBL UserChedBL, IUserBL UserBL, IScheduleBL scheduleBL)
           {
            _UserBL = UserBL;
            _userSchedBL = UserChedBL;
            _scheduleBL = scheduleBL;
        }
        // GET: UserScheduleController
        public async Task<ActionResult> Index(UserScheduleSearchInpuntDTO UserChed) 
        {
            var list = await _userSchedBL.Search(UserChed);
            return View(list);
        }

        // GET: UserScheduleController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var userSchedule = await _userSchedBL.GetById(Id);
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
                int result = await _userSchedBL.Create(pUserSched);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pUserSched);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: UserScheduleController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var UserSchedule = await _userSchedBL.GetById(Id);
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
                if (!ModelState.IsValid)
                    return View(pUserSchedule);
                int result = await _userSchedBL.Update(pUserSchedule);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else 
                {
                    ViewBag.ErrorMessage = "Error al Modificar registro. ";
                    return View(pUserSchedule);
                };
            }
            catch
            {
                return View();
            }
        }

        // GET: UserScheduleController/Delete/5
        public async  Task<ActionResult> Delete(int Id)
        {
            ScheduleSearchInputDTO sched = new ScheduleSearchInputDTO();
            userSearchInputDTO userId = new userSearchInputDTO();
            var schedList = await _scheduleBL.Search(sched);
            var userList = await _UserBL.Search(userId);

            ViewBag.ErrorMessage = "";
            ViewBag.Usuarios = userList;
            ViewBag.Horarios = schedList;

            var UserSchedule = await _userSchedBL.GetById(Id);
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
                if (!ModelState.IsValid)
                    return View(pUserSchedule);
                int result = await _userSchedBL.Delete(Id);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error al Eliminar registro. ";
                    return View(pUserSchedule);
                };
            }
            catch
            {
                return View(pUserSchedule);
            }
        }
    }
}
