using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.PatientDTO;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.UserSchedule;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public class UserSchedulesBL : IUserSchedulesBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public UserSchedulesBL(IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "UserSchedule";
            return ApiUrlBase;
        }
        public async Task<int> Create(UserScheduleInputDTO pUerChed)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonUserSchedule = JsonSerializer.Serialize(pUerChed);
                StringContent content = new StringContent(JsonUserSchedule, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var UserSchedule = JsonSerializer.Deserialize<DTOGenericResponse<UserScheduleSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.DeleteAsync(ApiUrlBase + $"/{id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var UserSchedule = JsonSerializer.Deserialize<DTOGenericResponse<UserScheduleSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<UserScheduleSearchOutputDTO> GetById(int id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var UserSchedule = JsonSerializer.Deserialize<DTOGenericResponse<UserScheduleSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return UserSchedule.Data;
                }
                return new UserScheduleSearchOutputDTO();
            }
            catch (Exception e)
            {
                return new UserScheduleSearchOutputDTO();
            }
        }

        public async Task<List<UserScheduleSearchOutputDTO>> Search(UserScheduleSearchInpuntDTO pUserChed)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var UserSchedule = JsonSerializer.Deserialize<DTOGenericResponse<List<UserScheduleSearchOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return UserSchedule.Data;
                }
                return new List<UserScheduleSearchOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<UserScheduleSearchOutputDTO>();
            }
        }
        public async Task<int> Update(UserScheduleInputDTO pUserChed)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pUserChed.UserScheduleId;

                string JsonUserSchedule = JsonSerializer.Serialize(pUserChed);
                StringContent content = new StringContent(JsonUserSchedule, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Appointments = JsonSerializer.Deserialize<DTOGenericResponse<UserScheduleSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
