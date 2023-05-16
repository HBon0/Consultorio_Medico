﻿using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.Interfaces
{
    public interface IWorkPlaceBL
    {
        Task<int> Create(WorkPlaceAddDTO pPWork);

        Task<int> Update(UpdateWorkPlaceDTO pWork);

        Task<int> Delete(int Id);

        Task<GetWorkPlaceByIdDTO> GetById(int Id);

        Task<List<WorkPlaceSearchOutPutDTO>> Search(WokplaceSearchInputDTO pWork);
    }
}
