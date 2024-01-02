using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infraestructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350, State= "Madrid", ZipCode = "MOCK", CVV = "MOCK", CardName = "MOCK", CardNumber = "1234", Expiration = "MOCK", PaymentMethod = 124 }
            };
        }
    }
}
