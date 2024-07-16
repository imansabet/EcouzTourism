using EcouzTourism.Domain.Entities;
using EcouzTourism.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcouzTourism.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
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

                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = $"{ obj.Name } Has been created successfully";
                return RedirectToAction("Index");
            }
            return View(); 
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u=>u.Id == villaId);
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

                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = $"{obj.Name } Has been updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Delete (int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(u => u.Id == obj.Id);
    
            if (objFromDb is not null)
            {

                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = $"{ objFromDb.Name } Has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "${ objFromDb } was not found.";

            return View();
        }
    }
}
