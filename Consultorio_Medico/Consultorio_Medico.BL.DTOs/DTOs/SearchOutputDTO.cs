using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.DTOs
{
    public class SearchOutputDTO
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }

        public string OfficeEmail { get; set; }
    }
}
