﻿using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Application.Utility;
using EcouzTourism.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcouzTourism.Controllers
{

    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public BookingController(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [Authorize] 
        public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = _userManager.FindByIdAsync(userId).GetAwaiter().GetResult();


            Booking booking = new()
            {
                VillaId = villaId,
                Villa = _unitOfWork.Villa.Get(u=>u.Id==villaId,includeProperties:"VillaAmenity"),
                CheckInDate = checkInDate,
                Nights = nights,
                CheckOutDate = checkInDate.AddDays(nights),
                UserId = userId,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Name = user.Name

            };
            booking.TotalCost = booking.Villa.Price * nights;

            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {
            var villa = _unitOfWork.Villa.Get(u => u.Id == booking.VillaId);
            booking.TotalCost = villa.Price * booking.Nights;

            booking.Status = SD.StatusPending;
            booking.BookingDate = DateTime.Now;

            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();
            return RedirectToAction(nameof(BookingConfirmation),new { bookingId = booking.Id});

            
           

        }
        [Authorize]
        public IActionResult BookingConfirmation(int bookingId)
        {
            

            return View(bookingId);
        }
    }
}
