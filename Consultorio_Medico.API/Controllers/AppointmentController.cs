using Consultorio_Medico.API.DTOs;
using Consultorio_Medico.BL.DTOs.AppointmentDTO;
using Consultorio_Medico.BL.DTOs.UserSchedule;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Medico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBL _appointmentBL;
        private readonly ILogger<AppointmentController> _logger;
        DTOGenericResponse<List<AppointmentSearchOutputDTO>> ListDTOresponse = new DTOGenericResponse<List<AppointmentSearchOutputDTO>>();
        DTOGenericResponse<AppointmentSearchOutputDTO> DTOGenResponse = new DTOGenericResponse<AppointmentSearchOutputDTO>();

        public AppointmentController(IAppointmentBL appointmentBL, ILogger<AppointmentController> logger)
        {
            _appointmentBL = appointmentBL;
            _logger = logger;
        }

        
        // GET: api/<AppointmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
