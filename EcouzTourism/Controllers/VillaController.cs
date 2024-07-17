﻿using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Domain.Entities;
using EcouzTourism.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcouzTourism.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepo;

        public VillaController(IVillaRepository villaRepo)
        {
            _villaRepo = villaRepo;
        }
        public IActionResult Index()
        {
            var villas = _villaRepo.GetAll();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {

                _villaRepo.Add(obj);
                _villaRepo.Save();
                TempData["success"] = $"{ obj.Name } Has been created successfully";
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaRepo.Get(u=>u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {

                _villaRepo.Update(obj);
                _villaRepo.Save();
                TempData["success"] = $"{obj.Name } Has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete (int villaId)
        {
            Villa? obj = _villaRepo.Get(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _villaRepo.Get(u => u.Id == obj.Id);
    
            if (objFromDb is not null)
            {

                _villaRepo.Remove(objFromDb);
                _villaRepo.Save();
                TempData["success"] = $"{ objFromDb.Name } Has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "${ objFromDb } was not found.";

            return View();
        }
    }
}
