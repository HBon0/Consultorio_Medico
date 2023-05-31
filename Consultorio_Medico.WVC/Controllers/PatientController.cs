using Consultorio_Medico.BL;
using Consultorio_Medico.BL.DTOs.PatientDTO;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using Consultorio_Medico.BL.Interfaces;
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
        private readonly ILogger<PatientController> _logger;
        public PatientController(IPatientBL patientBL, ILogger<PatientController> logger)
        {
            _patientBL = patientBL;
            _logger = logger;
        }
        // GET: PatientController
        public async Task<ActionResult> Index(patientSearchInputDTO patient)
        {
            _logger.LogInformation("-------------- INICIO METODO INDEX PATIENT CONTROLLER ----------------------");
            var list = await _patientBL.Search(patient);
            _logger.LogInformation("--------------- FIN METODO INDEX PATIENT CONTROLLER ------------------");
            return View(list);
        }

        // GET: PatientController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("------------- INICIO METODO DETAILS PATIENT CONTROLLER -------------------");
            var Patient = await _patientBL.GetById(Id);
            _logger.LogInformation("-------------- FIN METODO DETAILS PATIENT CONTROLLER ---------------------");
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
                _logger.LogInformation("----------------- INICIO METODO CREATE POST PATIENT CONTROLLER ---------------");
                int result = await _patientBL.Create(patient);
                if (result > 0)
                {
                    _logger.LogInformation("------------ REGISTRO CREADO CORRECTAMENTE EN CREATE POST PATIENT CONTROLLER ------------");
                    return RedirectToAction(nameof(Index));
                }
                    
                else
                {
                    _logger.LogInformation("---------------- ERROR AL TRATAR DE CREAR REGISTRO EN CREATE POST PATIENT CONTROLLER ---------");
                    ViewBag.ErrorMessage = "ERROR: NO SE REGISTRO";
                    return View(patient);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("------------ ERROR : " + ex.Message + " -----------------");
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("-------------- INICIO METODO EDIT GET PATIENT CONTROLLER --------------------");
            var Patient = await _patientBL.GetById(Id);
            _logger.LogInformation("-------------- FIN METODO EDIT GET PATIENT CONTROLLER ----------------");
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
                _logger.LogInformation("------------------ INICIO METODO EDIT POST PATIENT CONTROLLER ----------------------");
                int result = await _patientBL.Update(pPatient);
                if (result > 0)
                {
                    _logger.LogInformation("------------- REGISTRO EDITADO CORRECTAMENTE: EDIT POST PATIENT CONTROLLER -----------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("------------ ERROR AL TRATAR DE EDITAR EL REGISTRO: EDIT POST PATIENT CONTROLLER");
                    ViewBag.ErrorMessage = "ERROR: NO SE MODIFICO";
                    return View(pPatient);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("------------- ERROR : " + ex.Message + " -------------------------");
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
                _logger.LogInformation("------------ INICIO METODO DELETE POST PATIENT CONTROLLER -------------------");
                int result = await _patientBL.Delete(Id);
                if (result != 0)
                {
                    _logger.LogInformation("------------ REGISTRO ELIMINADO CORRECTAMENTE: DELETE POST PATIENT CONTROLLER ----------------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogInformation("-------------- ERROR AL ELIMINAR REGISTRO: DELETE POST PATIENT CONTROLLER -----------------");
                    ViewBag.ErrorMessage = "Error no se pudo Eliminar";
                    return View(pPatient);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("-------------- ERROR : " + ex.Message + " -------------------");
                return View(pPatient);
            }
        }
    }
}
