using CrisisApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class GetCoordinatesCommand : IRequest<Coordinatesinfo>
    {
        public string Id { get; private set; }

        public GetCoordinatesCommand(string id)
        {
            Id = id;
        }
    }

    public class GetCoordinatesCommandHandler : IRequestHandler<GetCoordinatesCommand, Coordinatesinfo>
    {
        private CrisisDbContext _dbContext;

        public GetCoordinatesCommandHandler(CrisisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Coordinatesinfo> Handle(GetCoordinatesCommand request, CancellationToken cancellationToken)
        {
            return await _dbContext.FindAsync<Coordinatesinfo>(request.Id);
        }
    }
}
