
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
            try
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
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> Delete(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.DeleteAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<WorkPlaceSearchOutPutDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
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
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<List<WorkPlaceSearchOutPutDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Workplaces.Data;
                }
                return new List<WorkPlaceSearchOutPutDTO>();
            }
            catch (Exception e)
            {
                return new List<WorkPlaceSearchOutPutDTO>();
            }
        }

        public async Task<int> Update(WorkPlaceInputDTO pWork)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pWork.WorkPlacesId;

                string JsonWorkPlace = JsonSerializer.Serialize(pWork);
                StringContent content = new StringContent(JsonWorkPlace, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<WorkPlaceSearchOutPutDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
