using Microsoft.Extensions.Logging;
using Ordering.Domain;

namespace Ordering.Infraestructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order() {UserName = "swn", FirstName = "Pablo", LastName = "Sato", EmailAddress = "pablofersato@gmail.com", AddressLine = "MOCK", Country = "Spain", TotalPrice = 350}
        };
    }
}
