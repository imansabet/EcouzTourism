using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Application.Services.Interface;
using EcouzTourism.Application.Utility;
using EcouzTourism.Models;
using EcouzTourism.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;

namespace EcouzTourism.Controllers
{
    public class HomeController : Controller
    {

        private readonly IVillaService _villaService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IVillaService villaService, IWebHostEnvironment webHostEnvironment)
        {
            _villaService = villaService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _villaService.GetAllVillas(),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {

            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                VillaList = _villaService.GetVillasAvailabilityByDate(nights, checkInDate),
                Nights = nights
            };

            return PartialView("_VillaList", homeVM);
        }

      

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
