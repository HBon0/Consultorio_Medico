using Consultorio_Medico.BL.DTOs.DoctorSpecialtiesDTO;
using Consultorio_Medico.BL.DTOs.DTOGenericResponse;
using Consultorio_Medico.BL.DTOs.DTOs;
using Consultorio_Medico.BL.DTOs.userDTO;
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
using System.Xml.Linq;

namespace Consultorio_Medico.BL
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;
        private readonly IDoctorSpecialtiesBL _doctorSpecialties;
        private readonly ISecurityDAL _securityDAL;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        HttpClient client = new HttpClient();

        public UserBL(IUserDAL userDAL, IUnitOfWork unitOfWork, ISecurityDAL security, IDoctorSpecialtiesBL doctorSpecialties, IConfiguration config)
        {

            _securityDAL = security;
            _userDAL = userDAL;
            _doctorSpecialties = doctorSpecialties;
            _unitOfWork = unitOfWork;
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
                Users UserEn = await _userDAL.GetById(pUser.UserId);
                if (UserEn.UserId == pUser.UserId)
                {
                    UserEn.UserId = pUser.UserId;
                    UserEn.RolId = pUser.RolId ;
                    UserEn.WorkplaceId = pUser.WorkplaceId;
                    UserEn.Name = pUser.Name;
                    UserEn.LastName = pUser.LastName;
                    UserEn.PhoneNumber = pUser.PhoneNumber;
                    UserEn.Dui = pUser.Dui;
                    UserEn.Email = pUser.Email;
                    UserEn.Login = pUser.Login;
                    UserEn.Status = pUser.Status;

                    bool ExistLogin = _securityDAL.ValidateLogin(UserEn);
                    if (ExistLogin)
                        throw new ArgumentException("El Login ya existe");

                    _userDAL.Update(UserEn);
                    return await _unitOfWork.SaveChangesAsync();

                }
                else return 0;
            }
            catch (Exception e)
            {

                throw;
            }

         }
        }


}

