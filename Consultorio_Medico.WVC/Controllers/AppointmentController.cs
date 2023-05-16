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
        public AppointmentController (IAppointmentBL appointmentBL, IUserBL userBL, ISpecialtieBL specialtieBL, IPatientBL petientBL)
        {
            _appointmentBL = appointmentBL;
            _userBL = userBL;
            _specialtieBL = specialtieBL;
            _patientBL = petientBL;
        }
        // GET: AppointmentController
        public async Task<ActionResult> Index(AppointmentSearchInputDTO pAppoinment)
        {
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
                Name = s.Name,
                Reason = s.Reason,
                Shift = s.Shift,
                Status = s.Status,
            }));
            ViewBag.apointments = list;
            return View(list);
        }

        // GET: AppointmentController/Details/5
        public async Task<ActionResult> Details(int Id)
        {
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointments = await _appointmentBL.GetById(Id);
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
                //if (!ModelState.IsValid)
                //    return View(pAppointment);
                int result = await _appointmentBL.Create(pAppointment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error al tratar de guardar el registro";
                    return View(pAppointment);
                }
            }
            catch
            {
                return View(pAppointment);
            }
        }

        // GET: AppointmentController/Edit/5
        public async Task<ActionResult> Edit(int Id)
        {
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointment = await _appointmentBL.GetById(Id);
            return View(new AppointmentInputDTO()
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                SpecialtieId = appointment.SpecialtieId,
                PatientId = appointment.PatientId,
                Name = appointment.Name,
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
                //if (!ModelState.IsValid)
                //    return View(pAppointment);
                int result = await _appointmentBL.Update(pAppointment);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error al tratar de Editar el registro";
                    return View(pAppointment);
                }
            }
            catch
            {
                return View(pAppointment);
            }
        }

        // GET: AppointmentController/Delete/5
        public async  Task<ActionResult> Delete(int Id)
        {
            userSearchInputDTO user = new userSearchInputDTO();
            var Users = await _userBL.Search(user);
            var Specialties = await _specialtieBL.GetAll();
            var Patients = await _patientBL.GetAll();

            ViewBag.Users = Users;
            ViewBag.Specialties = Specialties;
            ViewBag.Patients = Patients;

            var appointments = await _appointmentBL.GetById(Id);
            return View(appointments);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, AppointmentSearchOutputDTO pAppointment)
        {
            try
            {
                int result = await _appointmentBL.Delete(Id);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewBag.ErrorMessage = "Error al tratar de Eliminar el registro";
                    return View(pAppointment);
                }
            }
            catch
            {
                return View(pAppointment);
            }
        }
    }
}
