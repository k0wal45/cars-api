using Cars.Domain;
using Cars.Infrastructure;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;

namespace Cars.Application.Cars;

public class Create
{
    public class Command : IRequest<Car>
    {
        public required Car Car { get; set; }
    }

    public class Handler : IRequestHandler<Command, Car>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }
        public async Task<Car> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Cars.Add(request.Car);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Car;
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