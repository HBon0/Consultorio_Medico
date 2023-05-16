using Consultorio_Medico.BL.DTOs.UserSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.Interfaces
{
    public interface IUserSchedulesBL
    {
        Task<int> Create(userScheduleAddDTO pUerChed);

        Task<int> Update(userScheduleUpdateDTO pUserChed);

        Task<int> Delete(int id);

        Task<userScheduleGetByIdDTO> GetById(int id);

        Task<List<UserScheduleSearchOutputDTO>> Search(UserScheduleSearchInpuntDTO pUserChed);

    }
}
