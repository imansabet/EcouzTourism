using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Models;
using EcouzTourism.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcouzTourism.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll(),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),

            };
            return View(homeVM);
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
