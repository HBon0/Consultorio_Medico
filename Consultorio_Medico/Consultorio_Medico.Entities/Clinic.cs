using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.Entities
{
    public class Clinic
    {
        public int Id { get; set; } //:3

        public int UserId { get; set; } 
        public string OfficeName { get; set; }

        public string Address { get; set; }

        public string OfficeEmail  { get; set; } 

        public int PhoneNumber { get; set; } 

       
    }
}
