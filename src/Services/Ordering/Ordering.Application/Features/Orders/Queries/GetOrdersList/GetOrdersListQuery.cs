using MediatR;
using Ordering.Domain;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery: IRequest<List<OrdersVm>>
{
    public string UserName {get; set;}

    public GetOrdersListQuery(string userName)
    {
        userName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
}
