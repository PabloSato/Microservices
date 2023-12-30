using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain;

namespace Ordering.Application.Mappings;

public class MappingProfile : Profile
{
    protected MappingProfile()
    {
        CreateMap<Order, OrdersVm>().ReverseMap();
    }
}
