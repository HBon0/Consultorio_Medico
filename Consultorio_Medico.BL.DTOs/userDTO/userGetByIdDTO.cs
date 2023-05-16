using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.DTOs.userDTO
{
    public class userGetByIdDTO
    {
        public int UserId { get; set; }
        public int RolId { get; set; }
        public int WorkPlacesId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhonNumber { get; set; }
        public string Dui { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public byte Status { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
