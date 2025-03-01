﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Infrastructure.Verbatims;
using Microsoft.Extensions.Logging;
using Models.Db;
using Models.Db.Account;
using Models.Dtos;
using Models.DTOs;
using Models.DTOs.WorkerAccountDtos;
using Npgsql.Logging;
using Services.Abstractions;
using Services.AutoMapperProfiles;
using Services.Implementations;

namespace Seeder
{
    public class SeedData
    {
        private ICourierAccountService _courierAccountService;
        private IManagerAccountService _managerAccountService;
        private IAccountRoleService _accountRoleService;

        private IRestaurantRepository _restaurantRepository;
        private ILatLngRepository _latLngRepository;
        private IZoneRepository _zoneRepository;
        private DeliveryService _deliveryService;
        private OrderService _orderService;

        public SeedData()
        {
            Context = new TitsDbContext();

            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TitsAutomapperProfile())));

            var courierAccountRepository = new CourierAccountRepository(Context);
            var managerAccountRepository = new ManagerAccountRepository(Context);
            var accountRepository = new AccountRepository(Context);
            var workerRoleRepository = new WorkerRoleRepository(Context);
            var workerToRoleRepository = new WorkerToRoleRepository(Context);
            var restaurantRepository = new RestaurantRepository(Context);
            var sosRequestRepository = new SosRequestRepository(Context);
            var orderRepository = new OrderRepository(Context);
            _zoneRepository = new ZoneRepository(Context);
            var deliveryRepository = new DeliveryRepository(Context);
            _restaurantRepository = restaurantRepository;
            _latLngRepository = new LatLngRepository(Context);

            _courierAccountService = new CourierAccountService(courierAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, sosRequestRepository, mapper);
            _managerAccountService = new ManagerAccountService(managerAccountRepository, workerRoleRepository, workerToRoleRepository, restaurantRepository, mapper);
            _accountRoleService = new AccountRoleService(accountRepository, workerRoleRepository, workerToRoleRepository, mapper);
            _deliveryService = new DeliveryService(orderRepository, deliveryRepository, courierAccountRepository, _latLngRepository, mapper, new LoggerFactory().CreateLogger<DeliveryService>(), _restaurantRepository);
            _orderService = new OrderService(mapper, orderRepository, _restaurantRepository, _latLngRepository, _deliveryService);
        }

        private TitsDbContext Context { get; set; }

        public async Task Seed()
        {
            SeedAccountRoles();


            // ДоДо пицца на Крахмалёва 49
            // LAT: 53.26324200333876 LNG: 34.34160463381722

            // Create an entity
            var latLng = new LatLng() {Lat = 53.26324200333876f, Lng = 34.34160463381722f};
            await _latLngRepository.Insert(latLng);


            var zone = await SeedZone();

            // Save it with a relation
            var restaurant = new Restaurant() {LocationLatLngId = latLng.Id, AddressString = "ул. Крахмалева, 49, Брянск, Брянская обл., 241050", ZoneId = zone.Id};
            await _restaurantRepository.Insert(restaurant);

            zone.RestaurantId = restaurant.Id;
            await _zoneRepository.Update(zone);

            // Save back reference id
            latLng.RestaurantLocationId = restaurant.Id;
            await _latLngRepository.Update(latLng);

            var courier1Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "+79158052315", Password = "1", Username = "Александр Лемешов", RestaurantId = restaurant.Id})).Id;
            var courier2Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "+79003590767", Password = "1", Username = "Данила Поляков", RestaurantId = restaurant.Id})).Id;
            var courier3Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "C", Password = "C", Username = "Testing", RestaurantId = restaurant.Id})).Id;
            // var courier3Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "Courier3", Password = "Courier3", Username = "Misha3", RestaurantId = restaurant.Id})).Id;
            // var courier4Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "Courier4", Password = "Courier4", Username = "Misha4", RestaurantId = restaurant.Id})).Id;
            // var courier5Id = (await _courierAccountService.CreateCourier(new CreateCourierAccountDto() {Login = "Courier5", Password = "Courier5", Username = "Misha5", RestaurantId = restaurant.Id})).Id;

            var managerId = (await _managerAccountService.CreateManager(new CreateManagerAccountDto() {Login = "Manager", Password = "Manager", Username = "Vitaliy"})).Id;

            await _accountRoleService.AddToRole(courier1Id, WorkerRolesVerbatim.Courier);
            await _accountRoleService.AddToRole(courier2Id, WorkerRolesVerbatim.Courier);
            await _accountRoleService.AddToRole(courier3Id, WorkerRolesVerbatim.Courier);
            // await _accountRoleService.AddToRole(courier3Id, WorkerRolesVerbatim.Courier);
            // await _accountRoleService.AddToRole(courier4Id, WorkerRolesVerbatim.Courier);
            // await _accountRoleService.AddToRole(courier5Id, WorkerRolesVerbatim.Courier);
            await _accountRoleService.AddToRole(managerId, WorkerRolesVerbatim.Manager);

            // var delivery1Id = (await _deliveryService.BeginDelivery(new BeginDeliveryDto() {CourierId = courier1Id, OrderId = order1Id})).Id;
            // var delivery2Id = (await _deliveryService.BeginDelivery(new BeginDeliveryDto() {CourierId = courier2Id, OrderId = order2Id})).Id;
            // var delivery3Id = (await _deliveryService.BeginDelivery(new BeginDeliveryDto() {CourierId = courier3Id, OrderId = order3Id})).Id;
            // var delivery4Id = (await _deliveryService.BeginDelivery(new BeginDeliveryDto() {CourierId = courier4Id, OrderId = order4Id})).Id;
            // var delivery5Id = (await _deliveryService.BeginDelivery(new BeginDeliveryDto() {CourierId = courier5Id, OrderId = order5Id})).Id;
            
            // await _deliveryService.AddDeliveryLocation(new AddDeliveryLocationDto(delivery3Id, new LatLngDto() {Lat = 53.30440121711519f, Lng = 34.30663108022984f}));
            // await _deliveryService.AddDeliveryLocation(new AddDeliveryLocationDto(delivery4Id, new LatLngDto() {Lat = 53.31164006638071f, Lng = 34.30607711553764f}));
            // await _deliveryService.AddDeliveryLocation(new AddDeliveryLocationDto(delivery5Id, new LatLngDto() {Lat = 53.31276248184245f, Lng = 34.29251702324832f}));
        }

        private void SeedAccountRoles()
        {
            Context.WorkerRoles.AddRange(
                from accountRoleEn in WorkerRolesVerbatim.EnToRu.Keys
                select new WorkerRole {TitleEn = accountRoleEn, TitleRu = WorkerRolesVerbatim.EnToRu[accountRoleEn]}
            );
            Context.SaveChanges();
            Console.WriteLine("Seeded account roles");
        }

        private async Task<Zone> SeedZone()
        {
            Zone zone = new Zone();

            await _zoneRepository.Insert(zone);

            // {lat:53.263692, lng:34.339379},{lat:53.263769, lng: 34.344400},{lat:53.261696, lng:34.344690},{lat:53.260977, lng:34.340087}
            LatLng[] points =
            {
                new() {Lat = 53.263692f, Lng = 34.339379f, ZoneId = zone.Id},
                new() {Lat = 53.263769f, Lng = 34.344400f, ZoneId = zone.Id},
                new() {Lat = 53.261696f, Lng = 34.344690f, ZoneId = zone.Id},
                new() {Lat = 53.260977f, Lng = 34.340087f, ZoneId = zone.Id},
            };

            await _latLngRepository.Insert(points);

            return zone;
        }
    }
}