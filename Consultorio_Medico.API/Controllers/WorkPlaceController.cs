using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Medico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IWorkPlaceBL _workPlaceBL;
        private readonly ILogger<WorkPlaceController> _logger;
        
        public WorkPlaceController (IWorkPlaceBL workPlaceBL, ILogger<WorkPlaceController> logger)
        {

            _workPlaceBL = workPlaceBL;
            _logger = logger;
        }

        // GET: api/<WorkPlaceController>
        [HttpGet]
        public async Task<List<WorkPlaceSearchOutPutDTO>> Get()
        {
            _logger.LogInformation("------------------------------ INICIO DE METODO GET WORKPLACES --------------------------------");

            WokplaceSearchInputDTO wokplace = new WokplaceSearchInputDTO();
            var Workplaces = await _workPlaceBL.Search(wokplace);

            return Workplaces;
        }

        // GET api/<WorkPlaceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WorkPlaceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WorkPlaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorkPlaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
