using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.PatientDTO;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Consultorio_Medico.MVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador,Doctor")]
    public class PatientController : Controller
    {
        readonly IPatientBL _patientBL;
        public PatientController(IPatientBL patientBL)
        {
          _patientBL = patientBL;
        }
        // GET: PatientController
        public async Task<ActionResult> Index(patientSearchInputDTO patient)
        {
            var list = await _patientBL.Search(patient);
            return View(list);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            var Patient = await _patientBL.GetById(Id);

            return View(Patient);
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(patientAddDTO patient)
        {
            try
            {
                int result = await _patientBL.Create(patient);
                if (result > 0)

                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(patient);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            var Patient = await _patientBL.GetById(Id);

            return View(new patientAddDTO
            {
                patientId = Patient.patientId,
                Name= Patient.Name,
                LastName= Patient.LastName, 
                Telefono= Patient.Telefono, 
                DUI= Patient.DUI,   
            });
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,patientAddDTO pPatient)
        {
            try
            {
                int result = await _patientBL.Update(pPatient);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(pPatient);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            var Patient = await _patientBL.GetById(Id);
            return View(Patient);
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Id, patientSearchOutputDTO pPatient)
        {
            try
            {               
                int result = await _patientBL.Delete(Id);
                if (result != 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error no se pudo Eliminar";
                    return View(pPatient);
                }
            }
            catch
            {
                return View(pPatient);
            }
        }
    }
}
