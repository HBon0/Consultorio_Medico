using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.DTOGenericResponse
{
    public class DTOGenericResponse <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        
    }
}
