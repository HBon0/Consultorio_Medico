using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.UserSchedule
{
    public class userScheduleAddDTO
    {
          
        public int UserId { get; set; }    
        public int SchedulesId { get; set; }

       
    }
}
