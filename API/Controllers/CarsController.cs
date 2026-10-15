using Cars.Api.Controllers;
using Cars.Domain;
using Cars.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.API.Controllers;

public class CarsController : BaseApiController
{
    private readonly DataContext _context;
    public CarsController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetCars()
    {
        return await _context.Cars.AsNoTracking().ToListAsync();
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Car>> GetCar(Guid id)
    {
        var car = await _context.Cars.AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id);
        if (car == null) return NotFound();
        return car;
    }

    [HttpPost]
    public async Task<ActionResult<Car>> CreateCar(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCar), new { id = car.ID }, car);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return NotFound();
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCar(Guid id, Car car)
    {
        var existingCar = await _context.Cars.FindAsync(id);

        if (existingCar == null) return NotFound();

        existingCar.Brand = car.Brand;
        existingCar.Model = car.Model;
        existingCar.DoorsNumber = car.DoorsNumber;
        existingCar.LuggageCapacity = car.LuggageCapacity;
        existingCar.EngineCapacity = car.EngineCapacity;
        existingCar.FuelType = car.FuelType;
        existingCar.ProductionDate = car.ProductionDate;
        existingCar.AverageFuelConsumption = car.AverageFuelConsumption;
        existingCar.BodyType = car.BodyType;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}