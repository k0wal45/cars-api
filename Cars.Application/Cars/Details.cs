using Cars.Domain;
using Cars.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cars.Application.Cars;

public class Details
{
    public class Query : IRequest<Car?>
    {
        public Guid Id { get; set; }
    }
    public class Handler : IRequestHandler<Query, Car?>
    {
        private readonly DataContext _context;
        public Handler(DataContext context)
        {
            _context = context;
        }
        public async Task<Car?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Cars.AsNoTracking().FirstOrDefaultAsync(x => x.ID == request.Id, cancellationToken);
        }
    }
}
