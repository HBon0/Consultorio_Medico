﻿using Consultorio_Medico.BL.DTOs.RolDTO;
using Consultorio_Medico.BL.DTOs.WorkPlaceDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public class WorkPlaceBL : IWorkPlaceBL
    {

        readonly IWorkPlaceDAL _WorkPlaceDAL;
        readonly IUnitOfWork _unitOfWork;

        public WorkPlaceBL(IWorkPlaceDAL workPlaceDAL, IUnitOfWork unitOfWork)
        {
            _WorkPlaceDAL = workPlaceDAL;
            _unitOfWork = unitOfWork;
        }
        public async  Task<int> Create(WorkPlaceAddDTO pWork)
        {
            WorkPlace WorkEN = new WorkPlace()
            {
                WorkPlaces=pWork.WorkPlaces,

            };
            _WorkPlaceDAL.Create(WorkEN);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            WorkPlace WorkEN = await _WorkPlaceDAL.GetById(Id);
            if (WorkEN.WorkPlacesId == Id)
            {
                _WorkPlaceDAL.Delete(WorkEN);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<GetWorkPlaceByIdDTO> GetById(int Id)
        {

            WorkPlace WorkEN = await _WorkPlaceDAL.GetById(Id);
            return new GetWorkPlaceByIdDTO()
            {

                WorkPlacesId = WorkEN.WorkPlacesId,
                WorkPlaces = WorkEN.WorkPlaces,
            };
        }
    

        public async  Task<List<WorkPlaceSearchOutPutDTO>> Search(WokplaceSearchInputDTO pWork)
        {
            List<WorkPlace> WorkP = await _WorkPlaceDAL.Search(new WorkPlace { WorkPlaces = pWork.WorkPlacesLike, WorkPlacesId = pWork.WorkplacesIdLike });
            List<WorkPlaceSearchOutPutDTO> list = new List<WorkPlaceSearchOutPutDTO>();
            WorkP.ForEach(s => list.Add(new WorkPlaceSearchOutPutDTO
            {

                WorkPlacesId=s.WorkPlacesId,
                WorkPlaces = s.WorkPlaces,
              

            }));
            return list;
        }

        public async Task<int> Update(UpdateWorkPlaceDTO pWork)
        {
            WorkPlace WorkEN = await _WorkPlaceDAL.GetById(pWork.WorkPlacesId);
            if (WorkEN.WorkPlacesId == pWork.WorkPlacesId)
            {
                WorkEN.WorkPlaces = pWork.WorkPlaces;
                
                _WorkPlaceDAL.Update(WorkEN);
                return await _unitOfWork.SaveChangesAsync();

            }
            else return 0;
        }
    }
}
