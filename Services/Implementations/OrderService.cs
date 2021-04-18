﻿using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using Models.Db;
using Models.Dtos;
using Models.DTOs.Misc;
using Services.Abstractions;

namespace Services.Implementations
{
    public class OrderService : IOrderService
    {
        private IMapper _mapper;

        private IOrderRepository _orderRepository;
        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IRestaurantRepository restaurantRepository, ILatLngRepository latLngRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _restaurantRepository = restaurantRepository;
            _latLngRepository = latLngRepository;
        }

        public async Task<CreatedDto> Create(CreateOrderDto createOrderDto)
        {
            var restaurant = await _restaurantRepository.GetById(createOrderDto.RestaurantId);

            if (restaurant == null)
            {
                throw new("Restaurant not found");
            }

            var latLng = _mapper.Map<LatLng>(createOrderDto.Destination);

            await _latLngRepository.Insert(latLng);

            var order = _mapper.Map<Order>(createOrderDto);

            order.DestinationLatLngId = latLng.Id;
            await _orderRepository.Insert(order);

            return new CreatedDto(order.Id);
        }
    }
}