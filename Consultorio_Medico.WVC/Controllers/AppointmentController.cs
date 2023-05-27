using Consultorio_Medico.BL.DTOs.AppointmentDTO;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consultorio_Medico.MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBL _appointmentBL;
        private readonly IUserBL _userBL;
        private readonly ISpecialtieBL _specialtieBL;
        private readonly IPatientBL _patientBL;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController (IAppointmentBL appointmentBL, IUserBL userBL, ISpecialtieBL specialtieBL, IPatientBL petientBL, ILogger<AppointmentController> logger)
        {
            _appointmentBL = appointmentBL;
            _userBL = userBL;
            _specialtieBL = specialtieBL;
            _patientBL = petientBL;
            _logger = logger;
        }
        // GET: AppointmentController
        public async Task<ActionResult> Index(AppointmentSearchInputDTO pAppoinment)
        {
            _logger.LogInformation("---------- INICIO METODO INDEX APPOINTMENT CONTROLLER ---------");
            List<AppointmentSearchInputDTO> list = new List<AppointmentSearchInputDTO>();
            
            var appointments = await _appointmentBL.Search(pAppoinment);
            appointments.ForEach(s => list.Add( new AppointmentSearchInputDTO()
            {
                AppointmentId = s.AppointmentId,
                UserId = s.UserId,
                SpecialtieId = s.SpecialtieId,
                PatientId = s.PatientId,
                UserName = s.UserName,
                SpecialtieName = s.SpecialtieName,
                PatientName = s.PatientName,
                Appointment_Name = s.Appointment_Name,
                Reason = s.Reason,
                Shift = s.Shift,
                Status = s.Status,
            }));
            ViewBag.apointments = list;
            _logger.LogInformation("--------- FIN METODO INDEX APPOINTMENT CONTROLLER -------------");
            return View(list);
        }

        // GET: AppointmentController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            _logger.LogInformation("------------ INICIO METODO DETAILS APPOINTMENT CONTROLLER ---------");
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointments = await _appointmentBL.GetById(Id);
            _logger.LogInformation(" ---------- FIN METODO DETAILS APPOINTMENT CONTROLLER ----------");
            return View(appointments);
        }

        // GET: AppointmentController/Create
        public async Task<ActionResult> Create()
        {
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppointmentInputDTO pAppointment)
        {
            try
            {
                _logger.LogInformation("--------- INICIO METODO CREATE POST APPOINTMENT CONTROLLER ------------");
                //if (!ModelState.IsValid)
                //    return View(pAppointment);
                int result = await _appointmentBL.Create(pAppointment);
                if (result > 0) {
                    _logger.LogInformation("------- REGISTRO CREADO : CREATE POST APPOINTMENT CONTROLLER ---------");
                    return RedirectToAction(nameof(Index));
                }
                    
                else
                {
                    _logger.LogWarning("--- ERROR AL CREAR EL REGISTRO : CREATE POST APPOINTMENT CONTROLLER ------");
                    ViewBag.ErrorMessage = "Error al tratar de guardar el registro";
                    return View(pAppointment);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("-------- ERROR : " + e.Message + " -----------");
                return View(pAppointment);
            }
        }

        // GET: AppointmentController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            _logger.LogInformation("---------- INICIO METODO EDIT GET APPOINTMENT CONTROLLER ----------");
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointment = await _appointmentBL.GetById(Id);
            if (appointment is null)
            {
                _logger.LogWarning($"------- NO SE ENCONTRO REGISTRO CON ID {Id} : EDIT GET APPOINTMENT CONTROLLER ----");
                RedirectToAction(nameof(Index));
            }
            _logger.LogInformation("------- FIN EDIT GET APPOINTMENT CONTROLLER ----------");
            return View(new AppointmentInputDTO()
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                SpecialtieId = appointment.SpecialtieId,
                PatientId = appointment.PatientId,
                Appointment_Name = appointment.Appointment_Name,
                Reason = appointment.Reason,
                Appointment_date = appointment.Appointment_date,
                Appointment_Hour = appointment.Appointment_Hour,
                Shift = appointment.Shift,
                Status = appointment.Status,
            });
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, AppointmentInputDTO pAppointment)
        {
            try
            {
                _logger.LogInformation("-------- INICIO METODO EDIT POST APPOINTMENT CONTROLLER ----------------");
                //if (!ModelState.IsValid)
                //    return View(pAppointment);
                int result = await _appointmentBL.Update(pAppointment);
                if (result > 0) {
                    _logger.LogInformation("----- REGISTRO EDITADO : EDIT POST APPOINTMENT CONTROLLER -----------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("--- NO SE PUDO EDITAR: EDITAR POST APPOINTMENT CONTROLLER ----------");
                    ViewBag.ErrorMessage = "Error al tratar de Editar el registro";
                    return View(pAppointment);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("------- ERROR: " + e.Message + " ------------");
                return View(pAppointment);
            }
        }

        // GET: AppointmentController/Delete/5
        public async  Task<ActionResult> Delete(int Id)
        {
            _logger.LogInformation("----- INICIO METODO DELETE GET APPOINTMENT CONTROLLER ---------");
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointments = await _appointmentBL.GetById(Id);
            if (appointments is null) {
                _logger.LogWarning($"---- REGISTRO CON ID {Id} NO ENCONTRADO: DELETE GET APPOINTMENT CONTROLLER --------");
                RedirectToAction(nameof(Index));
            }
            _logger.LogInformation("------------ FIN METODO DELETE GET APPOINTMENT CONTROLLER ---------");
            return View(appointments);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, AppointmentSearchOutputDTO pAppointment)
        {
            try
            {
                _logger.LogInformation("------- INICIO METODO DELETE POST APPOINTMENT CONTROLLER -----------");
                int result = await _appointmentBL.Delete(Id);
                if (result > 0) {
                    _logger.LogInformation("------- REGISTRO ELIMINADO : DELETE POST APPOINTMENT CONTROLLER ----------");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogWarning("------ NO SE PUDO ELIMINAR : DELETE POST APPOINTMENT CONTROLLER -----------");
                    ViewBag.ErrorMessage = "Error al tratar de Eliminar el registro";
                    return View(pAppointment);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR: " + ex.Message + " ----------");
                return View(pAppointment);
            }
        }
    }
}
