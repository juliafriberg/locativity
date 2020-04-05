using CrisisApi.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class GetIdentifierCommand : IRequest<IdentifierObject>
    {
        public GetIdentifierCommand()
        {
        }
    }

    public class GetIdentifierCommandHandler : IRequestHandler<GetIdentifierCommand, IdentifierObject>
    {
        public Task<IdentifierObject> Handle(GetIdentifierCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new IdentifierObject());
        }
    }
}
