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
    public class GetAllCoordinatesByDateCommand : IRequest<List<Coordinatesinfo>>
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public GetAllCoordinatesByDateCommand(DateTime start, DateTime end) 
        {
            Start = start;
            End = end;
        }
    }

    public class GetAllCoordinatesByDateCommandHandler : IRequestHandler<GetAllCoordinatesByDateCommand, List<Coordinatesinfo>>
    {
        private CrisisDbContext _dbContext;

        public GetAllCoordinatesByDateCommandHandler(CrisisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Coordinatesinfo>> Handle(GetAllCoordinatesByDateCommand request, CancellationToken cancellationToken)
        {
            return await _dbContext.Coordinatesinfo.Where(x => x.Addedtime >= request.Start
            && x.Addedtime <= request.End).ToListAsync();
        }
    }
}
