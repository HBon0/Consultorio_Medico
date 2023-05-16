using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
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
        readonly IScheduleBL _scheduleBL;


        public SchedulesController(IScheduleBL scheduleBL)
        {
            _scheduleBL = scheduleBL;
        }

        
        // GET: SchedulesController
        public async Task<ActionResult> Index(ScheduleSearchInputDTO schedules)
        { 


            var list = await _scheduleBL.Search(schedules);
            return View(list);
        }

        // GET: SchedulesController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            var Schedules = await _scheduleBL.GetById(Id);
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
        public async Task<ActionResult> Create(AddScheduleDTO schedule)
        {

            try
            {
                int result = await _scheduleBL.Create(schedule);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(schedule);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SchedulesController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            GetScheduleByIdDTO schedule = await _scheduleBL.GetById(Id);
            var SchedResult = new UpdateScheduleDTO()
            {
                SchedulesId = schedule.ScheduleId,
                DayName = schedule.DayName,
                StartShift = schedule.StartOfShift,   
                EndOfShift=schedule.EndOfShift,

            };
            return View(SchedResult);
        }

        // POST: SchedulesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateScheduleDTO schedule)
        {
            try
            {
                int result = await _scheduleBL.Update(schedule);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(schedule);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: SchedulesController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {

            GetScheduleByIdDTO customer = await _scheduleBL.GetById(Id);
            return View(customer);
        }

        // POST: SchedulesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Delete(int Id, GetScheduleByIdDTO schedule)
        {
            try
            {
                int result = await _scheduleBL.Delete(Id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(schedule);
                }
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();

            }
  
    }
  }
}
