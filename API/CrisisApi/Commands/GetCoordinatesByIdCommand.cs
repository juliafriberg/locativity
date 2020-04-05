using CrisisApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class GetCoordinatesByIdCommand : IRequest<List<Coordinatesinfo>>
    {
        public Guid Id { get; private set; }

        public GetCoordinatesByIdCommand(Guid id)
        {
            Id = id;
        }
    }

    public class GetCoordinatesByIdCommandHandler : IRequestHandler<GetCoordinatesByIdCommand, List<Coordinatesinfo>>
    {
        private CrisisDbContext _dbContext;

        public GetCoordinatesByIdCommandHandler(CrisisDbContext _dbContext)
        {

        }

        public Task<List<Coordinatesinfo>> Handle(GetCoordinatesByIdCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
