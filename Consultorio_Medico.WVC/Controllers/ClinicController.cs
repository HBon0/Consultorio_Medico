using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.DTOs;
using Consultorio_Medico.Entities.Interfaces;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Consultorio_Medico.WVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ClinicController : Controller
    {
        readonly IClinicBL _clinicBL;


        public ClinicController(IClinicBL clinicBL)
        {
            _clinicBL = clinicBL;
        }

        // GET: ClinicController
        public async Task<IActionResult> Index(SearchinputDTO clinic)
        {

            var list = await _clinicBL.Search(clinic);
            return View(list);

        }

        // GET: ClinicController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            GetByIdOutputDTO clinic = await _clinicBL.GetById(id);

            return View(clinic);
        }

        // GET: ClinicController/Create
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClinicController/Create
        [HttpPost]

        public async Task<ActionResult> Create(CreateInputDTO pClinic)
        {
            try
            {
                int result = await _clinicBL.Create(pClinic);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(pClinic);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        //GET: ClinicController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            GetByIdOutputDTO clinics = await _clinicBL.GetById(id);
            var ClinicResults = new UpdateDTO()
            {
                ClincisId = clinics.ClincisId,
                UserId = clinics.UserId,
                OfficeName = clinics.OfficeName,            
                OfficeAddres = clinics.OfficeAddres,
                OfficeEmail = clinics.OfficeEmail,
                OfficePhone = clinics.OfficePhone,


            };
            return View(clinics);
        }

        // POST: ClinicController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int id, UpdateDTO clinic)
        {
            try
            {
                int result = await _clinicBL.Update(clinic);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Error no se pudo modificar";
                    return View(clinic);
                }
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
               return View();
            }
        }

        // GET: ClinicController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            GetByIdOutputDTO  clinic = await _clinicBL.GetById(id); 
            return View(clinic);
        }

        // POST: ClinicController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, GetByIdOutputDTO clinic)
        {
            try
            {
                int result = await _clinicBL.Delete(id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE ELIMINO";
                    return View(clinic);
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
