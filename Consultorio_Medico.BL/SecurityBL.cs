using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Consultorio_Medico.BL
{
    public class SecurityBL: ISecurityBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public SecurityBL (IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Security";
            return ApiUrlBase;
        }
        public Users ChangePassword(Users users, string PasswordAnt)
        {
            throw new NotImplementedException();
        }

        public async Task<securityDTO> Login(string Login, string Password)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += $"?Login={Login}&Password={Password}";
                var Credenciales = new { 
                    Login = Login,
                    Password = Password,
                };

                string JsonCredenciales = JsonSerializer.Serialize(Credenciales);
                StringContent content = new StringContent(JsonCredenciales, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Security = JsonSerializer.Deserialize<DTOGenericResponse<securityDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Security.Data;
                }
                return new securityDTO();
            }
            catch (Exception e)
            {
                return new securityDTO();
            }
        }
    }
}
