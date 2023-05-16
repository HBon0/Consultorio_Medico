using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio_Medico.Entities.Interfaces
{
    public interface IClinic

    {
        void Create(Clinic pClinic);
        void Update(Clinic pClinic);
        void Delete(Clinic pClinic);

        Task<Clinic> GetById(Clinic pClinic);

        Task<List<Clinic>> Search(Clinic pClinic);
       // Task <List<Clinic>> GetAll();
    }
}
