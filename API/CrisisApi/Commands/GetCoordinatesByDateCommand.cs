using CrisisApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class GetCoordinatesByDateCommand : IRequest<List<Coordinatesinfo>>
    {
        public Guid Id { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public GetCoordinatesByDateCommand(Guid id, DateTime start, DateTime end)
        {
            Id = id;
            Start = start;
            End = end;
        }
    }

    public class GetCoordinatesByDateCommandHandler : IRequestHandler<GetCoordinatesByDateCommand, List<Coordinatesinfo>>
    {
        private CrisisDbContext _dbContext;

        public GetCoordinatesByDateCommandHandler(CrisisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Coordinatesinfo>> Handle(GetCoordinatesByDateCommand request, CancellationToken cancellationToken)
        {
            return await _dbContext.Coordinatesinfo.Where(x => x.Id == request.Id 
            && x.Addedtime >= request.Start 
            && x.Addedtime <= request.End).ToListAsync();
        }
    }
}
