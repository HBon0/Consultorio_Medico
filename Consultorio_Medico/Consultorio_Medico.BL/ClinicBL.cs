using Consultorio_Medico.BL.DTOs.DTOs;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Consultorio_Medico.BL
{
    public class ClinicBL : IClinicBL
    {

        readonly IClinic clinicDAL;
        readonly IUnitOfWork unitOfWork;



        public ClinicBL(IClinic pClinicDAL, IUnitOfWork pUnitWork)
        {
            clinicDAL = pClinicDAL;
            unitOfWork = pUnitWork;
        }

        public async Task<CreateOutputDTO> Create(CreateInputDTO pClinic)
        {
            Clinic clinic = new Clinic
            {
                Address = pClinic.Address,
                OfficeEmail = pClinic.OfficeEmail,
                PhoneNumber = pClinic.PhoneNumber

            };
            clinicDAL.Create(clinic);
            await unitOfWork.SaveChangesAsync();
            CreateOutputDTO ClinicOutput = new CreateOutputDTO
            {
                Id = clinic.Id,
                Address = clinic.Address,
                OfficeEmail = clinic.OfficeEmail,
                PhoneNumber = clinic.PhoneNumber
            };
            return ClinicOutput;
        }

        public async Task<int> Delete(DeleteDTO pClinic)
        {
            Clinic clinic = await clinicDAL.GetById(new Clinic { Id = pClinic.Id });
            if (clinic.Id == pClinic.Id)
            {
                clinicDAL.Delete(clinic);
                return await unitOfWork.SaveChangesAsync();
            }
            else
                return 0;

        }

        public async Task<GetByIdOutputDTO> GetById(GetByIdInputDTO pClinic)
        {
            Clinic clinic = await clinicDAL.GetById(new Clinic { Id = pClinic.Id });
            return new GetByIdOutputDTO
            {
                Id = clinic.Id,
                OfficeName = clinic.OfficeName,
                Address = clinic.Address,
                OfficeEmail = clinic.OfficeEmail,
                PhoneNumber = clinic.PhoneNumber,
                UserId = clinic.UserId,
            };
        }

        public async Task<List<SearchOutputDTO>> Search(SearchinputDTO pClinic)
        {
            List<Clinic> clinics = await clinicDAL.Search(new Clinic { OfficeEmail = pClinic.OfficeEmail, OfficeName = pClinic.OfficeName });
            List<SearchOutputDTO> list = new List<SearchOutputDTO>();
            clinics.ForEach(s => list.Add(new SearchOutputDTO
            {
                Id = s.Id,
                OfficeEmail = s.OfficeEmail,
                OfficeName = s.OfficeName,

            }));
            return list;
        }
      


        public async Task<int> Update(UpdateDTO pClinic)
        {
          Clinic clinic = await clinicDAL.GetById(new Clinic { Id = pClinic.Id });
            if (clinic.Id == pClinic.Id)
            {
                clinic.OfficeName = pClinic.OfficeEmail;
                clinicDAL.Update(clinic);
                return await unitOfWork.SaveChangesAsync(); 
            }
            else
                return 0;
        }
      }
 }

