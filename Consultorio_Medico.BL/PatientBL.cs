using Consultorio_Medico.BL.DTOs.AppointmentDTO;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.PatientDTO;
using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
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
    public class PatientBL : IPatientBL
    {
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();
        public PatientBL(IConfiguration config)
        {
            _configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Patient";
            return ApiUrlBase;
        }

        public async Task<int> Create(patientAddDTO pPatient)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonPatient = JsonSerializer.Serialize(pPatient);
                StringContent content = new StringContent(JsonPatient, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Patients = JsonSerializer.Deserialize<DTOGenericResponse<patientSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
                    var Patients = JsonSerializer.Deserialize<DTOGenericResponse<patientSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<patientSearchOutputDTO> GetById(int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Patients = JsonSerializer.Deserialize<DTOGenericResponse<patientSearchOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Patients.Data;
                }
                return new patientSearchOutputDTO();
            }
            catch (Exception e)
            {
                return new patientSearchOutputDTO();
            }
        }

        public async Task<List<patientSearchOutputDTO>> Search(patientSearchInputDTO pPatient)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Patients = JsonSerializer.Deserialize<DTOGenericResponse<List<patientSearchOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Patients.Data;
                }
                return new List<patientSearchOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<patientSearchOutputDTO>();
            }

        }

        public async Task<int> Update(patientAddDTO pPatient)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pPatient.patientId;

                string JsonAppointment = JsonSerializer.Serialize(pPatient);
                StringContent content = new StringContent(JsonAppointment, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Appointments = JsonSerializer.Deserialize<DTOGenericResponse<patientSearchOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
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
