using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consultorio_Medico.BL.DTOs.RolDTO
{
    public class RolAddDTO
    {
       
        public string Name { get; set; }
        public byte Status { get; set; }
       

        //[Required(ErrorMessage = "Nombre es obligatorio.")]
        //[StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        //public string? Name { get; set;}

        //[Required(ErrorMessage = "Estado es obligatorio.")]
        //[Display(Name = "status")]
        //public string? status { get; set;}

    }
}
