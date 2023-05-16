using Consultorio_Medico.BL.DTOs.ScheduleDTO;
using Consultorio_Medico.BL.DTOs.userDTO;
using Consultorio_Medico.BL.Interfaces;
using Consultorio_Medico.Entities;
using Consultorio_Medico.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL
{
    public class ScheduleBL:IScheduleBL
    {

        readonly IScheduleDAL _scheduleDAL;
        readonly IUnitOfWork _unitOfWork;

        public ScheduleBL(IScheduleDAL ScheduleDAL, IUnitOfWork unitOfWork)
        {
            _scheduleDAL = ScheduleDAL;
            _unitOfWork = unitOfWork;


        }

        public async Task<int> Create(AddScheduleDTO Schedule)
        {
            Schedules schedules = new Schedules()
            {
                DayName = Schedule.DayName,
                StartOfShift = Schedule.StarShift,
                EndOfShift = Schedule.EndOfShift,
                                         
            };
            _scheduleDAL.Create(schedules);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            Schedules SchedEN = await _scheduleDAL.GetById(Id);
            if (SchedEN.SchedulesId == Id)
            {
                _scheduleDAL.Delete(SchedEN);
                return await _unitOfWork.SaveChangesAsync();
            }
            else
                return 0;


        }

        public async Task<GetScheduleByIdDTO> GetById(int Id)
        {
          Schedules schedEN = await _scheduleDAL.GetById(Id);
            return new GetScheduleByIdDTO()
            {
               ScheduleId= schedEN.SchedulesId,
               DayName= schedEN.DayName,
                StartOfShift = schedEN.StartOfShift,
               EndOfShift= schedEN.EndOfShift,  

            };

        }

        public async Task<List<ScheduleSearchOutPutDTO>> Search(ScheduleSearchInputDTO Schedule)
        {
            List<Schedules> schedules = await _scheduleDAL.Search(new Schedules { DayName = Schedule.DayNameLike, StartOfShift = Schedule.StartShiftLike, EndOfShift = Schedule.EndOfShiftLike });
            List<ScheduleSearchOutPutDTO> list = new List<ScheduleSearchOutPutDTO>();
            schedules.ForEach(s => list.Add(new ScheduleSearchOutPutDTO
            {
                SchedulesId=s.SchedulesId,
                DayName=s.DayName,
                StartOfShift = s.StartOfShift,
                EndOfShift = s.EndOfShift,


            }));
            return list;
        
    }



        public async Task<int> Update(UpdateScheduleDTO Schedule)
        {
            Schedules schedEN = await _scheduleDAL.GetById(Schedule.SchedulesId);
            if (schedEN.SchedulesId == Schedule.SchedulesId)
            {
                schedEN.SchedulesId = Schedule.SchedulesId;
                schedEN.DayName = Schedule.DayName;
                schedEN.StartOfShift = Schedule.StartShift;
                schedEN.EndOfShift = Schedule.EndOfShift;
                return await _unitOfWork.SaveChangesAsync();

            }
            else
                return 0;
        }

      
    }
}
