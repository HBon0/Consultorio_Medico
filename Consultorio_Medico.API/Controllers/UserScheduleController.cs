using Consultorio_Medico.API.DTOs;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.DTOs.UserSchedule;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Medico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScheduleController : ControllerBase
    {
        private readonly IUserSchedulesBL _userScheduleBL;
        private readonly ILogger<UserScheduleController> _logger;
        DTOGenericResponse<List<UserScheduleSearchOutputDTO>> ListDTOresponse = new DTOGenericResponse<List<UserScheduleSearchOutputDTO>>();
        DTOGenericResponse<UserScheduleSearchOutputDTO> DTOGenResponse = new DTOGenericResponse<UserScheduleSearchOutputDTO>();

        public UserScheduleController(IUserSchedulesBL userScheduleBL, ILogger<UserScheduleController> logger)
        {
            _userScheduleBL = userScheduleBL;
            _logger = logger;
        }



        // GET: api/<UserScheduleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserScheduleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserScheduleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserScheduleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserScheduleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
