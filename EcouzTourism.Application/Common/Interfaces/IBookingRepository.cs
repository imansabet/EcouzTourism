﻿using EcouzTourism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcouzTourism.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);

    }
}
