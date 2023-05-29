using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.SpecialtiesDTO;
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

namespace Consultorio_Medico.BL
{
    public class SpecialtiesBL : ISpecialtieBL
    {
        private readonly ISpecialtiesDAL _specialties;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _Configuration;
        HttpClient client = new HttpClient();

        public SpecialtiesBL (ISpecialtiesDAL specialties, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _specialties = specialties;
            _unitOfWork = unitOfWork;
            _Configuration = config;
        }
        public string GetUrlAPI()
        {
            string ApiUrlBase = _Configuration.GetValue<string>("ApiConnectionString");
            ApiUrlBase += "Specialtie";
            return ApiUrlBase;
        }

        public async Task<int> Create(SpecialtiesInputDTO pSpecialties)
        {
            try
            {
                string ApiUrl = GetUrlAPI();

                string JsonSpecialy = JsonSerializer.Serialize(pSpecialties);
                StringContent content = new StringContent(JsonSpecialy, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PostAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<SpecialtiesOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> Update (SpecialtiesInputDTO pSpecialties)
        {
            try
            {
                string ApiUrl = GetUrlAPI();
                ApiUrl += "/" + pSpecialties.Id;

                string JsonSpecialy = JsonSerializer.Serialize(pSpecialties);
                StringContent content = new StringContent(JsonSpecialy, Encoding.UTF8, "application/json");

                var HttpResponse = await client.PutAsync(ApiUrl, content);
                if (HttpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<SpecialtiesOutputDTO>>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> Delete (int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.DeleteAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<SpecialtiesOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return 1;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task<SpecialtiesOutputDTO> GetById (int Id)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase + $"/{Id}");

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Workplaces = JsonSerializer.Deserialize<DTOGenericResponse<SpecialtiesOutputDTO>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Workplaces.Data;
                }
                return new SpecialtiesOutputDTO();
            }
            catch (Exception e)
            {
                return new SpecialtiesOutputDTO();
            }
        }

        public async Task<List<SpecialtiesOutputDTO>> Search (SpecialtiesInputDTO pSpecialties)
        {
            try
            {
                string ApiUrlBase = GetUrlAPI();
                var HttpResponse = await client.GetAsync(ApiUrlBase);

                if (HttpResponse.IsSuccessStatusCode)
                {
                    var content = await HttpResponse.Content.ReadAsStringAsync();
                    var Specialties = JsonSerializer.Deserialize<DTOGenericResponse<List<SpecialtiesOutputDTO>>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Specialties.Data;
                }
                return new List<SpecialtiesOutputDTO>();
            }
            catch (Exception e)
            {
                return new List<SpecialtiesOutputDTO>();
            }
        }

        public async Task<List<SpecialtiesOutputDTO>> GetAll ()
        {
            List<SpecialtiesOutputDTO> specialtiesOutputDTOs = new List<SpecialtiesOutputDTO>();

            List<Specialties> specialties = await _specialties.GetAll();

            specialties.ForEach(s => specialtiesOutputDTOs.Add(new SpecialtiesOutputDTO
            {
                Id= s.SpecialtiesId,
                Specialty = s.Specialty
            }));
            return specialtiesOutputDTOs;
        }
    }
}
