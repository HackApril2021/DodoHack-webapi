﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Db;

namespace Infrastructure.Abstractions
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetById(long id);
        
        Task<ICollection<Delivery>> GetByOrderId(long orderId);
        
        Task<ICollection<Delivery>> GetByRestaurantId(long restaurantId);
        
        Task<ICollection<Delivery>> GetByCourierIdAndDate(long courierId, DateTime startTime, DateTime endTime);
        
        Task<ICollection<Delivery>> GetInProgressByCourier(long courierId);

        Task Update(Delivery delivery);

        Task Remove(Delivery delivery);

        Task Insert(Delivery delivery);
    }
}