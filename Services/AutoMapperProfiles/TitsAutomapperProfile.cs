using AutoMapper;
using Models.Db;
using Models.Db.Account;
using Models.Dtos;
using Models.DTOs.Responses;
using Models.DTOs.WorkerAccountDtos;

namespace Services.AutoMapperProfiles
{
    // --------------------------------------------------------- //
    // EVEN IF YOUR IDE SAYS THIS CODE IS UNUSED, DONT DELETE IT //
    // --------------------------------------------------------- //

    public class TitsAutomapperProfile : Profile
    {
        public TitsAutomapperProfile()
        {
            // ReverseMap() нужен для обратной конвертации любого мапа

            CreateMap<WorkerRoleDto, WorkerRole>().ReverseMap();
            CreateMap<CreateWorkerAccountDto, WorkerAccount>().ReverseMap();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(
                    dto => dto.DestinationLatLng,
                    cfg => cfg.Ignore())
                .ReverseMap();
        }
    }
}