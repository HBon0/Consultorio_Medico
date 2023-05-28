
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public class WorkPlaceBL : IWorkPlaceBL
    {

        readonly IWorkPlaceDAL _WorkPlaceDAL;
        readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WorkPlaceBL> _logger;
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public WorkPlaceBL(IWorkPlaceDAL workPlaceDAL, IUnitOfWork unitOfWork, ILogger<WorkPlaceBL> logger, IConfiguration config)
        {
            _WorkPlaceDAL = workPlaceDAL;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = config;
        }

        public string GetUrlAPI ()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "WorkPlace";
            return ApiUrlBase;
        }

        public async  Task<int> Create(WorkPlaceInputDTO pWork)
        {
            string ApiUrl = GetUrlAPI();

            string JsonWorkPlace = JsonSerializer.Serialize(pWork);
            StringContent content = new StringContent(JsonWorkPlace, Encoding.UTF8, "application/json");

            var HttpResponse = await client.PostAsync(ApiUrl, content);
            if (HttpResponse.IsSuccessStatusCode)
            {
                var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<WorkPlaceSearchOutPutDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int Id)
        {
            WorkPlace WorkEN = await _WorkPlaceDAL.GetById(Id);
            if (WorkEN.WorkPlacesId == Id)
            {
                _WorkPlaceDAL.Delete(WorkEN);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<WorkPlaceSearchOutPutDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<WorkPlaceSearchOutPutDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Workplaces.Data;
                }
                return new WorkPlaceSearchOutPutDTO();
            }
            catch (Exception e)
            {
                return new WorkPlaceSearchOutPutDTO();
            }
        }
    
        public async  Task<List<WorkPlaceSearchOutPutDTO>> Search(WokplaceSearchInputDTO pWork)
        {
            _logger.LogInformation("--------------- INICIO DE METODO SEARCH WORKPLACE -----------------------");
            List<WorkPlace> WorkP = await _WorkPlaceDAL.Search(new WorkPlace { WorkPlaces = pWork.WorkPlaces, WorkPlacesId = pWork.WorkplacesId });
            List<WorkPlaceSearchOutPutDTO> list = new List<WorkPlaceSearchOutPutDTO>();
            WorkP.ForEach(s => list.Add(new WorkPlaceSearchOutPutDTO
            {

                WorkPlacesId=s.WorkPlacesId,
                WorkPlaces = s.WorkPlaces,
              

            }));
            return list;
        }

        public async Task<int> Update(WorkPlaceInputDTO pWork)
        {
            WorkPlace WorkEN = await _WorkPlaceDAL.GetById(pWork.WorkPlacesId);
            if (WorkEN.WorkPlacesId == pWork.WorkPlacesId)
            {
                WorkEN.WorkPlaces = pWork.WorkPlaces;
                
                _WorkPlaceDAL.Update(WorkEN);
                return await _unitOfWork.SaveChangesAsync();

            }
            else return 0;
        }
    }
}
