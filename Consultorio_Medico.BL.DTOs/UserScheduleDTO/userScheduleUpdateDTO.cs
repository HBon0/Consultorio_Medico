﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.UserSchedule
{
   public class userScheduleUpdateDTO
    {
        public int UserSchedulesId { get; set; }
        public int UserId { get; set; }
        public int SchedulesId { get; set; }
    }
}
