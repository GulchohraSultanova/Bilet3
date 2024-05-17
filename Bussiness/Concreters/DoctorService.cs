using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concreters
{
    public class DoctorService : IDoctorService
    {
        IDoctorRepository _doctorRepository;
        IWebHostEnvironment _webHostEnvironment;

        public DoctorService(IWebHostEnvironment webHostEnvironment, IDoctorRepository doctorRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _doctorRepository = doctorRepository;
        }

        public void Create(Doctor doctor)
        {
           if (doctor == null)
            {
                throw new NotFoundException("", "Null ola bilmez!");
            }
            if (!doctor.PhotoFile.ContentType.Contains("image/"))
            {
                throw new FileTypeContentException("PhotoFile", "Fayl tipi dogru deyil!");
            }
            string filename= doctor.PhotoFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + filename;
            using(FileStream file=new FileStream(path, FileMode.Create))
            {
                doctor.PhotoFile.CopyTo(file);
            }
            doctor.ImgUrl = filename;
            _doctorRepository.Add(doctor);
            _doctorRepository.Commit();
        }

        public void Delete(int id)
        {
           var oldDoctor= _doctorRepository.Get(x=>x.Id==id);
            string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + oldDoctor.ImgUrl;
            FileInfo fileInfo= new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _doctorRepository.Delete(oldDoctor);
            _doctorRepository.Commit();


        }

        public List<Doctor> GetAllDoctors(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.GetAll(func);
        }

        public Doctor GetDoctor(Func<Doctor, bool>? func = null)
        {
            return _doctorRepository.Get(func);
        }

        public void Update(int id,Doctor doctor)
        {
            Doctor updateDoctor = _doctorRepository.Get(x=>x.Id==id);
            if (updateDoctor == null)
            {
                throw new NotFoundException("", "Null ola bilmez!");
            }
            if(doctor.PhotoFile != null)
            {
                if (!doctor.PhotoFile.ContentType.Contains("image/"))
                {
                    throw new FileTypeContentException("PhotoFile", "Fayl tipi dogru deyil!");
                }
                string filename = doctor.PhotoFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\Upload\Doctor\" + filename;
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    doctor.PhotoFile.CopyTo(file);
                }
                updateDoctor.ImgUrl = filename;
            }
            else
            {
                doctor.ImgUrl = updateDoctor.ImgUrl;
            }
            updateDoctor.Name=doctor.Name;

            updateDoctor.Position=doctor.Position;
            _doctorRepository.Commit();
        }
    }
}
