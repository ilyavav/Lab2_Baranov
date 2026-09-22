using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_Baranov.Data;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("cars-with-clients")]
    public async Task<IActionResult> GetCarsWithClients()
    {
        var cars = await _context.Cars
            .Select(car => new
            {
                car.Id,
                car.Brand,
                car.Model,
                car.Year,
                car.LicensePlate,
                car.Mileage,
                car.ClientId,
                ClientName = car.Client!.Name,
                ClientPhone = car.Client.Phone
            })
            .ToListAsync();

        return Ok(cars);
    }

    [HttpGet("orders-with-details")]
    public async Task<IActionResult> GetOrdersWithDetails()
    {
        var orders = await _context.RepairOrders
            .Select(order => new
            {
                order.Id,
                order.Description,
                order.CreatedDate,
                order.WorkCost,
                order.PartsCost,
                order.Status,
                CarBrand = order.Car!.Brand,
                CarModel = order.Car.Model,
                LicensePlate = order.Car.LicensePlate,
                ClientName = order.Car.Client!.Name,
                ClientPhone = order.Car.Client.Phone
            })
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("active-orders")]
    public async Task<IActionResult> GetActiveOrders()
    {
        var orders = await _context.RepairOrders
            .Where(order => order.Status != "Completed")
            .Select(order => new
            {
                order.Id,
                order.Description,
                order.Status,
                order.CreatedDate,
                CarBrand = order.Car!.Brand,
                CarModel = order.Car.Model,
                LicensePlate = order.Car.LicensePlate
            })
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("client-orders/{clientId}")]
    public async Task<IActionResult> GetClientOrders(int clientId)
    {
        var orders = await _context.RepairOrders
            .Where(order => order.Car!.ClientId == clientId)
            .Select(order => new
            {
                order.Id,
                order.Description,
                order.CreatedDate,
                order.WorkCost,
                order.PartsCost,
                order.Status,
                CarBrand = order.Car!.Brand,
                CarModel = order.Car.Model,
                LicensePlate = order.Car.LicensePlate,
                ClientName = order.Car.Client!.Name
            })
            .ToListAsync();

        return Ok(orders);
    }
}