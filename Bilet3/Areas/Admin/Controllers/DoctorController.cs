using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bilet3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var doctors = _service.GetAllDoctors();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor) { 
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _service.Create(doctor);
            }
            catch (NotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            catch(FileTypeContentException ex)
            {
                ModelState.AddModelError("PhotoFile", ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var doctor=_service.GetDoctor(x=>x.Id == id);
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _service.Update(doctor.Id,doctor);
            }
            catch (NotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            catch (FileTypeContentException ex)
            {
                ModelState.AddModelError("PhotoFile", ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {

            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
