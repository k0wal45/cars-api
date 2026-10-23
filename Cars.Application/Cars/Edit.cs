using Cars.Domain;
using Cars.Infrastructure;
using MediatR;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace Cars.Application.Cars;

public class Edit
{
    public class Command : IRequest<bool>
    {
        public Guid Id { get; set; }
        public required Car Car { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var car = await _context.Cars.FindAsync(new object[] { request.Id }, cancellationToken);

            if (car is null) return false;

            car.Brand = request.Car.Brand;
            car.Model = request.Car.Model;
            car.DoorsNumber = request.Car.DoorsNumber;
            car.LuggageCapacity = request.Car.LuggageCapacity;
            car.EngineCapacity = request.Car.EngineCapacity;
            car.FuelType = request.Car.FuelType;
            car.ProductionDate = request.Car.ProductionDate;
            car.AverageFuelConsumption = request.Car.AverageFuelConsumption;
            car.BodyType = request.Car.BodyType;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Car).SetValidator(new CarValidator());
        }
    }

}
