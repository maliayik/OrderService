using ECommerce.OrderService.Application.Interfaces;
using ECommerce.OrderService.Application.Services;
using ECommerce.OrderService.Core.Entity;
using ECommerce.OrderService.Core.Interfaces;
using ECommerce.OrderService.Infrastructure;
using ECommerce.OrderService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("OrderServiceDb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Seed data için
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Id = 1, Name = "Keyboard", Stock = 100, Price = 10.99m },
            new Product { Id = 2, Name = "Mouse", Stock = 50, Price = 20.99m },
            new Product { Id = 3, Name = "Headset", Stock = 200, Price = 5.99m }
        );
    }

    if (!context.Customers.Any())
    {
        context.Customers.AddRange(
            new Customer { Id = 1, Name = "Mehmet", Address = "Istanbul" },
            new Customer { Id = 2, Name = "Ahmet", Address = "Ankara" },
            new Customer { Id = 3, Name = "Fatma", Address = "Izmir" }
        );
    }

    context.SaveChanges();
}

app.Run();