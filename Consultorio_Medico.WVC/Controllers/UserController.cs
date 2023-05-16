using Microsoft.AspNetCore.Mvc;
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

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class UserController : Controller
    {
        readonly IUserBL _UserBL;
        readonly IRolBL _rolBL;
        private readonly ISpecialtieBL _specialtieBL;
        private readonly IScheduleBL _scheduleBL;
        readonly IWorkPlaceBL _workPlaceBL;


        public UserController(IUserBL UserBL, IRolBL rolBL, IWorkPlaceBL workPlaceBL, ISpecialtieBL specialtieBL, IScheduleBL scheduleBL)
        {
            _UserBL = UserBL;
            _rolBL = rolBL; 
            _workPlaceBL = workPlaceBL;
            _specialtieBL = specialtieBL;
            _scheduleBL = scheduleBL;
        }

        // GET: UserController
        public async Task<IActionResult> Index(userSearchInputDTO user)
        {
            var list = await _UserBL.Search(user);
            return View(list);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            RolSearchingInputDTO rol = new RolSearchingInputDTO();
            WokplaceSearchInputDTO workpalce = new WokplaceSearchInputDTO();
            ScheduleSearchInputDTO schedule = new ScheduleSearchInputDTO();
            SpecialtiesInputDTO specialtie = new SpecialtiesInputDTO();


            var specialtiesList = await _specialtieBL.Search(specialtie);
            var scheduleList = await _scheduleBL.Search(schedule);
            var Rollist = await _rolBL.Search(rol);
            var worklist = await _workPlaceBL.Search(workpalce);
            userGetByIdDTO user = await _UserBL.GetById(Id);

            ViewBag.ErrorMessage = "";
            ViewBag.Specialties = specialtiesList;
            ViewBag.Schedules = scheduleList;
            ViewBag.Roles = Rollist;
            ViewBag.Workplace = worklist;

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
                int result = await _UserBL.Create(pUser);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pUser);
                }

            }
            catch (Exception ex)
            {
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

            userGetByIdDTO Users = await _UserBL.GetById(Id);
            var UserResults = new userUpdateDTO()
            {
                
                UserId = Users.UserId,
                IdRol = Users.RolId,
                WorkplacesId = Users.WorkPlacesId,
                Name = Users.Name,  
                LastName = Users.LastName,
                PhonNumber = Users.PhonNumber,
                Dui = Users.Dui,
                Email = Users.Email,
                Login = Users.Login,
                Status =Users.Status,

            };
            return View(UserResults);
        }

        // POST: UserController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int Id, userUpdateDTO User)
        {
            try
            {
                int result = await _UserBL.Update(User);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Error no se pudo modificar";
                    return View(User);
                }
            }
            catch (Exception ex)
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

                ViewBag.ErrorMessage = ex.Message;
                return View(User);
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int Id)
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

            userGetByIdDTO User = await _UserBL.GetById(Id);
            return View(User);
        }

        //  UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, userGetByIdDTO pUser)
        {
            try
            {
                int result = await _UserBL.Delete(Id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(pUser);
                }
            }
            catch (Exception ex)
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

                ViewBag.ErrorMessage = ex.Message;
                return View(pUser);
            }
        }
    }
}
