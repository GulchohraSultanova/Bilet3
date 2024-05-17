using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstracts
{
    public  interface  IDoctorService
    {
        void Create(Doctor doctor);
        void Delete(int  id);
        void Update(int id,Doctor doctor);
        Doctor GetDoctor(Func<Doctor,bool> ? func=null);
        List<Doctor> GetAllDoctors(Func<Doctor,bool> ? func=null);
    }
}
