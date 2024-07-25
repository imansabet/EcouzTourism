using EcouzTourism.Application.Common.Interfaces;
using EcouzTourism.Application.Utility;
using EcouzTourism.Domain.Entities;
using EcouzTourism.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcouzTourism.Infrastructure.Respository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Booking entity)
        {
            _db.Bookings.Update(entity);
        }

        public void UpdateStatus(int bookingId, string bookingStatus,int villaNumber=0) 
        {
            var bookingfromDb = _db.Bookings.FirstOrDefault(m => m.Id == bookingId);
            if (bookingfromDb != null)
            {
                bookingfromDb.Status = bookingStatus;
                if(bookingStatus == SD.StatusCheckedIn)
                {
                    bookingfromDb.VillaNumber = villaNumber;
                    bookingfromDb.ActualCheckInDate = DateTime.Now;
                }
                if(bookingStatus == SD.StatusCompleted)
                {
                    bookingfromDb.ActualCheckOutDate = DateTime.Now;
                }

            }
        }

        public void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId)
        {
            var bookingfromDb = _db.Bookings.FirstOrDefault(m => m.Id == bookingId);
            if (bookingfromDb != null)
            {
                if (!string.IsNullOrEmpty(sessionId))
                {
                    bookingfromDb.StripeSessionId = sessionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    bookingfromDb.StripePaymentIntentId = paymentIntentId;
                    bookingfromDb.PaymentDate = DateTime.Now;
                    bookingfromDb.IsPaymentSuccessful = true;
                }
            }
        }
    }
}
