using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Application.Utility;
using EcouzTourism.Domain.Entities;
using EcouzTourism.Infrastructure.Data;
using EcouzTourism.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcouzTourism.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Amenitys = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(Amenitys);
        }
        public IActionResult Create()
        {
            AmenityVM AmenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            return View(AmenityVM);
        }
       

        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {

            
            if (ModelState.IsValid )
            {

                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity Has been created successfully";
                return RedirectToAction(nameof(Index));
            }
          
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj); 
        }
        public IActionResult Update(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }


        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The amenity Has been updated successfully";
                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            
            return View(amenityVM);
        }
        public IActionResult Delete(int amenityId)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityId)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? objFromDb = _unitOfWork.Amenity.Get
                (u => u.Id == amenityVM.Amenity.Id);

            if(objFromDb is not null)
            {
                _unitOfWork.Amenity.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "the amenity has been deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "The amenity could not be deleted";
            return View();
            
        }
    }
}
