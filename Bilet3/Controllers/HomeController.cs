
using Bussiness.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bilet3.Controllers
{
    public class HomeController : Controller
    {
       
        IDoctorService _doctorService;

        public HomeController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IActionResult Index()
        {
           List<Doctor> doctors = _doctorService.GetAllDoctors();
            return View(doctors);
        }

        

    }
}
