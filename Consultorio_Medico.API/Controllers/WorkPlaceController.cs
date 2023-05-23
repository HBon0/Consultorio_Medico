using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.DTOs;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Consultorio_Medico.API.DTOs;
using Consultorio_Medico.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Consultorio_Medico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IWorkPlaceBL _workPlaceBL;
        private readonly ILogger<WorkPlaceController> _logger;
        DTOGenericResponse<List<WorkPlaceSearchOutPutDTO>> ListDTOresponse = new DTOGenericResponse<List<WorkPlaceSearchOutPutDTO>>();
        DTOGenericResponse<WorkPlaceSearchOutPutDTO> DTOGenResponse = new DTOGenericResponse<WorkPlaceSearchOutPutDTO>();

        public WorkPlaceController (IWorkPlaceBL workPlaceBL, ILogger<WorkPlaceController> logger)
        {
            _workPlaceBL = workPlaceBL;
            _logger = logger;
        }

        // GET: api/<WorkPlaceController>
        [HttpGet]
        public async Task<DTOGenericResponse<List<WorkPlaceSearchOutPutDTO>>> Get()
        {
            _logger.LogInformation("------------- INICIO DE METODO GET WORKPLACES ----------------");
            
            WokplaceSearchInputDTO wokplace = new WokplaceSearchInputDTO();
            var Workplaces = await _workPlaceBL.Search(wokplace);

            var DTOGenResponse = ListDTOresponse.GetGenericResponse(true, "Obtencion de todos los registros", Workplaces);
            _logger.LogInformation("---- FIN METODO GET WORKPLACES ----");
            return DTOGenResponse;
        }

        // GET api/<WorkPlaceController>/5
        [HttpGet("{Id}")]
        public async Task<DTOGenericResponse<WorkPlaceSearchOutPutDTO>> GetById(int Id)
        {
            _logger.LogInformation("---- INICIO METODO GET BY ID WORKPLACES ----");
            var workplace = await _workPlaceBL.GetById(Id);

            var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Obteniendo Workplace", workplace);
            _logger.LogInformation("---- FIN METODO GET BY ID WORKPLACES ----");
            return pDTOGenResponse;
        }

        // POST api/<WorkPlaceController>
        [HttpPost]
        public async Task<DTOGenericResponse<WorkPlaceSearchOutPutDTO>> Post(WorkPlaceInputDTO pWorkplace)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO POST WORKPLACES ----");

                var workplace = await _workPlaceBL.Create(pWorkplace);
                if (workplace > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Creacion correcta", new WorkPlaceSearchOutPutDTO()
                    {
                        WorkPlacesId = pWorkplace.WorkPlacesId,
                        WorkPlaces = pWorkplace.WorkPlaces
                    });
                    _logger.LogInformation("---- FIN METODO POST WORKPLACES ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", new WorkPlaceSearchOutPutDTO()
                    {
                        WorkPlacesId = pWorkplace.WorkPlacesId,
                        WorkPlaces = pWorkplace.WorkPlaces
                    });
                    _logger.LogWarning("---- ERROR EN METODO POST WORKPLACES ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, new WorkPlaceSearchOutPutDTO()
                {
                    WorkPlacesId = pWorkplace.WorkPlacesId,
                    WorkPlaces = pWorkplace.WorkPlaces
                });
                return DTOGenRes;
            }
        }

        // PUT api/<WorkPlaceController>/5
        [HttpPut("{Id}")]
        public async Task<DTOGenericResponse<WorkPlaceSearchOutPutDTO>> Put(int Id, WorkPlaceInputDTO pWorkplace)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO PUT WORKPLACES ----");

                var workplace = await _workPlaceBL.Create(pWorkplace);
                if (workplace > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Modificacion correcta", new WorkPlaceSearchOutPutDTO()
                    {
                        WorkPlacesId = pWorkplace.WorkPlacesId,
                        WorkPlaces = pWorkplace.WorkPlaces
                    });
                    _logger.LogInformation("---- FIN METODO PUT WORKPLACES ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", new WorkPlaceSearchOutPutDTO()
                    {
                        WorkPlacesId = pWorkplace.WorkPlacesId,
                        WorkPlaces = pWorkplace.WorkPlaces
                    });
                    _logger.LogWarning("---- ERROR EN METODO PUT WORKPLACES ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, new WorkPlaceSearchOutPutDTO()
                {
                    WorkPlacesId = pWorkplace.WorkPlacesId,
                    WorkPlaces = pWorkplace.WorkPlaces
                });
                return DTOGenRes;
            }
        }

        // DELETE api/<WorkPlaceController>/5
        [HttpDelete("{Id}")]
        public async Task<DTOGenericResponse<WorkPlaceSearchOutPutDTO>> Delete(int Id)
        {
            try
            {
                _logger.LogInformation("---- INICIO METODO DELETE WORKPLACES ----");

                var workplace = await _workPlaceBL.Delete(Id);
                if (workplace > 0)
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(true, "Eliminacion correcta", null);
                    _logger.LogInformation("---- FIN METODO DELETE WORKPLACES ----");
                    return pDTOGenResponse;
                }
                else
                {
                    var pDTOGenResponse = DTOGenResponse.GetGenericResponse(false, "Error al crear", null);
                    _logger.LogWarning("---- ERROR EN METODO DELETE WORKPLACES ----");
                    return pDTOGenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("--- ERROR : " + ex.Message + " ----");
                var DTOGenRes = DTOGenResponse.GetGenericResponse(false, "Error : " + ex.Message, null);
                return DTOGenRes;
            }
        }
    }
}
