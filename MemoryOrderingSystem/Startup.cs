using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MemoryOrderingSystem.Data;
using Microsoft.EntityFrameworkCore;
using MemoryOrderingSystem.Services;
using MemoryOrderingSystem.Services.Interfaces;
using System.Text.Json.Serialization;

namespace MemoryOrderingSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MemoryOrderingContext>(options =>
                options.UseInMemoryDatabase("MemoryOrdering"));

            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen();

            services.AddScoped<SellerService>();
            services.AddScoped<OrderItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<SetupService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SetupService setupService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                setupService.Setup();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Memory Ordering System");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
