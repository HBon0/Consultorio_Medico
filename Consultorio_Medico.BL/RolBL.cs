

using Consultorio_Medico.BL.DTOs.RolDTO;

using Consultorio_Medico.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Consultorio_Medico.BL.DTOs.DTOs;
using Microsoft.Extensions.Configuration;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
using System.Text.Json;

namespace Consultorio_Medico.BL
{
    public class RolBL : IRolBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public RolBL(IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Rol";
            return ApiUrlBase;
        }

        public async Task<int> Create(RolInputDTO pRol)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonRol = JsonSerializer.Serialize(pRol);
                StringContent content = new StringContent(JsonRol, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Roles = JsonSerializer.Deserialize<DTOGenericResponse<RolSearchingOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                    var Roles = JsonSerializer.Deserialize<DTOGenericResponse<RolSearchingOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<List<RolSearchingOutputDTO>> Search(RolSearchingInputDTO pRol)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Roles = JsonSerializer.Deserialize<DTOGenericResponse<List<RolSearchingOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Roles.Data;
                }
                return new List<RolSearchingOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<RolSearchingOutputDTO>();
            }

        }

        public async Task<int> Update(RolInputDTO pRol)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pRol.RolId;

                string JsonRoles = JsonSerializer.Serialize(pRol);
                StringContent content = new StringContent(JsonRoles, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Roles = JsonSerializer.Deserialize<DTOGenericResponse<RolSearchingOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<RolSearchingOutputDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Rol = JsonSerializer.Deserialize<DTOGenericResponse<RolSearchingOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Rol.Data;
                }
                return new RolSearchingOutputDTO();
            }
            catch (Exception e)
            {
                return new RolSearchingOutputDTO();
            }
        }
    }
}

    


