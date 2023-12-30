using Discount.Grpc.Services;

namespace Discount.Grpc;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddAutoMapper(typeof(Startup));
        services.AddGrpc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<DiscountService>();
        });
    }
}
