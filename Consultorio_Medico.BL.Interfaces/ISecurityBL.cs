using Consultorio_Medico.BL.DTOs.userDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.BL.Interfaces
{
    public interface ISecurityBL
    {
        public Task<securityDTO> Login(string Login, string Password);
    }
}
