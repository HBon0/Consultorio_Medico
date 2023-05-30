using Consultorio_Medico.BL.DTOs.DoctorSpecialtiesDTO;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.DTOs;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.DTOs.UserSchedule;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
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
using System.Xml.Linq;

namespace Consultorio_Medico.BL
{
    public class UserBL : IUserBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public UserBL(IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "User";
            return ApiUrlBase;
        }

        public async Task<int> Create(UserAddDTO pUser)
        {
            #region BorrarLuego

            pUser.UserSchedules = new List<UserScheduleSearchInpuntDTO>();
            pUser.DoctorSpecialtie = new List<DoctorSpecialtiesInputDTO>(); 
            #endregion

            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonUser = JsonSerializer.Serialize(pUser);
                StringContent content = new StringContent(JsonUser, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var User = JsonSerializer.Deserialize<DTOGenericResponse<userSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                return 0;
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
                    var User = JsonSerializer.Deserialize<DTOGenericResponse<userSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public async Task<userSearchOutputDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var User = JsonSerializer.Deserialize<DTOGenericResponse<userSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return User.Data;
                }
                return new userSearchOutputDTO();
            }
            catch (Exception e)
            {
                return new userSearchOutputDTO();
            }

        }

        public async Task<List<userSearchOutputDTO>> Search(userSearchInputDTO pUser)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var User = JsonSerializer.Deserialize<DTOGenericResponse<List<userSearchOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return User.Data;
                }
                return new List<userSearchOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<userSearchOutputDTO>();
            }
        }

        public async Task<int> Update(userUpdateDTO pUser)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pUser.UserId;

                string JsonUser = JsonSerializer.Serialize(pUser);
                StringContent content = new StringContent(JsonUser, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Users = JsonSerializer.Deserialize<DTOGenericResponse<userSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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

