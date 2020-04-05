using CrisisApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class GetAllCoordinatesCommand : IRequest<List<Coordinatesinfo>>
    {
        public GetAllCoordinatesCommand() { }
    }

    public class GetAllCoordinatesCommandHandler : IRequestHandler<GetAllCoordinatesCommand, List<Coordinatesinfo>>
    {
        private CrisisDbContext _dbContext;

        public GetAllCoordinatesCommandHandler(CrisisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Coordinatesinfo>> Handle(GetAllCoordinatesCommand request, CancellationToken cancellationToken)
        {
            return await _dbContext.Coordinatesinfo.ToListAsync();
        }
    }
}
