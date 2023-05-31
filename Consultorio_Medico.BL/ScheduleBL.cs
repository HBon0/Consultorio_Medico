using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public class ScheduleBL : IScheduleBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public ScheduleBL(IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Schedules";
            return ApiUrlBase;
        }

        public async Task<int> Create(ScheduleInputDTO Schedule)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonSchedule = JsonSerializer.Serialize(Schedule);
                StringContent content = new StringContent(JsonSchedule, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Schedules = JsonSerializer.Deserialize<DTOGenericResponse<ScheduleSearchOutPutDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                    var Schedules = JsonSerializer.Deserialize<DTOGenericResponse<ScheduleSearchOutPutDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public async Task<ScheduleSearchOutPutDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Schedule = JsonSerializer.Deserialize<DTOGenericResponse<ScheduleSearchOutPutDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Schedule.Data;
                }
                return new ScheduleSearchOutPutDTO();
            }
            catch (Exception e)
            {
                return new ScheduleSearchOutPutDTO();
            }
        }

        public async Task<List<ScheduleSearchOutPutDTO>> Search(ScheduleSearchInputDTO Schedule)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Schedules = JsonSerializer.Deserialize<DTOGenericResponse<List<ScheduleSearchOutPutDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Schedules.Data;
                }
                return new List<ScheduleSearchOutPutDTO>();
            }
            catch (Exception e)
            {
                return new List<ScheduleSearchOutPutDTO>();
            }

        }

        public async Task<int> Update(ScheduleInputDTO Schedule)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + Schedule.SchedulesId;

                string JsonSchedule = JsonSerializer.Serialize(Schedule);
                StringContent content = new StringContent(JsonSchedule, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Schedules = JsonSerializer.Deserialize<DTOGenericResponse<ScheduleSearchOutPutDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
