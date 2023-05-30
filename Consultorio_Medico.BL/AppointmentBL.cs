using Consultorio_Medico.BL.DTOs.AppointmentDTO;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.ScheduleDTO;
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
    public class AppointmentBL : IAppointmentBL
    {
        private readonly IAppointmentDAL _appointment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();
        public AppointmentBL (IUnitOfWork unitOfWork, IAppointmentDAL appointment, IConfiguration config)
        {
            _appointment = appointment;
            _unitOfWork = unitOfWork;
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Appointment";
            return ApiUrlBase;
        }

        public async Task<int> Create (AppointmentInputDTO pAppointment)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonAppointment = JsonSerializer.Serialize(pAppointment);
                StringContent content = new StringContent(JsonAppointment, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Schedules = JsonSerializer.Deserialize<DTOGenericResponse<AppointmentSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                return 0;
            }
        }
        public async Task<int> Update(AppointmentInputDTO pAppointment)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pAppointment.AppointmentId;

                string JsonAppointment = JsonSerializer.Serialize(pAppointment);
                StringContent content = new StringContent(JsonAppointment, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Appointments = JsonSerializer.Deserialize<DTOGenericResponse<AppointmentSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                    var Appointment = JsonSerializer.Deserialize<DTOGenericResponse<AppointmentSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<AppointmentSearchOutputDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Appointments = JsonSerializer.Deserialize<DTOGenericResponse<AppointmentSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Appointments.Data;
                }
                return new AppointmentSearchOutputDTO();
            }
            catch (Exception e)
            {
                return new AppointmentSearchOutputDTO();
            }
        }
        
        public async Task<List<AppointmentSearchOutputDTO>> Search(AppointmentSearchInputDTO pAppointmentSearch)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Appointments = JsonSerializer.Deserialize<DTOGenericResponse<List<AppointmentSearchOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Appointments.Data;
                }
                return new List<AppointmentSearchOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<AppointmentSearchOutputDTO>();
            }
        }
    }
   
}
