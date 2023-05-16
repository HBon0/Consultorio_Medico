using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        readonly IWorkPlaceBL workPlaceBL;
        public WorkPlaceController(IWorkPlaceBL workplaceBL)
        {
            workPlaceBL = workplaceBL;
        }
        // GET: WorkPlaceController
        public async Task<ActionResult>  Index (WokplaceSearchInputDTO wokplace)
        {
          var list = await workPlaceBL.Search(wokplace);
            return View(list);
        }

        // GET: WorkPlaceController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            GetWorkPlaceByIdDTO workplace = await workPlaceBL.GetById(Id);
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
        public async Task<ActionResult> Create(WorkPlaceAddDTO pWork)
        {
            try
            {
                int result = await workPlaceBL.Create(pWork);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pWork);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: WorkPlaceController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            GetWorkPlaceByIdDTO Workplaces = await workPlaceBL.GetById(Id);
            var UserResults = new UpdateWorkPlaceDTO()
            {

                WorkPlacesId = Workplaces.WorkPlacesId,
                WorkPlaces = Workplaces.WorkPlaces,
       
            };
            return View(UserResults);
        }

        // POST: WorkPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, UpdateWorkPlaceDTO Workplace)
        {
            try
            {
                int result = await workPlaceBL.Update(Workplace);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Error no pudo modificar";
                    return View(Workplace) ;
                }
            }
            catch  (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        
        // GET: WorkPlaceController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            GetWorkPlaceByIdDTO Worlplace = await workPlaceBL.GetById(Id);
            return View(Worlplace);
        }
    
        // POST: WorkPlaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, GetWorkPlaceByIdDTO Workplace)
        {
            try
            {
               int result = await workPlaceBL.Delete(Id); 
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else {

                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                        return View(Workplace);
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

