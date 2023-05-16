using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ESFE.ArqLimpia.EN;
//using ESFE.ArqLimpia.EN.Interfaces;
//using Microsoft.EntityFrameworkCore;




namespace Consultorio_Medico.DAL
{
    public class ClinicDAL
    {
        public ClinicDAL(ClinicDALOptions<ClinicDAL> options) : base(options) { }
        public DbSet<User>User { get; set; }
        public DbSet<OfficeName> OfficeName { get; set; }
        public DbSet<OffiAddres> OffiAddres { get; set; }
        public DbSet<OfficeMail> OfficeMail { get; set; }
        public DbSet<PhoneNumber> PhoneNumber { get; set; }
       
    }
}
