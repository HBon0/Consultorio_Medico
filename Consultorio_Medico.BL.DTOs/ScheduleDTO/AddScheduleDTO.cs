using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.ScheduleDTO
{
    public class AddScheduleDTO
    {
     public string DayName { get; set; }    
     public decimal StarShift { get; set; }
     public decimal EndOfShift  { get; set; }


    }
}
