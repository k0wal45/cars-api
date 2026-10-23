using Cars.Infrastructure;
using MediatR;

namespace Cars.Application.Cars;

public class Delete
{
    public class Command : IRequest<bool>
    {
        public Guid Id { get; set; }
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
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}