using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



using Consultorio_Medico.BL.DTOs.DTOs;

namespace Consultorio_Medico.BL.Interfaces
{
    public interface IClinicBL
    {
        Task<CreateOutputDTO> Create(CreateInputDTO pClinic);

        Task<int> Update(UpdateDTO pClinic);

        Task <int> Delete(DeleteDTO pClinic);

        Task<GetByIdOutputDTO> GetById(GetByIdInputDTO pClinic);

        Task<List<SearchOutputDTO>> Search(SearchinputDTO pClinic);
    }
}
