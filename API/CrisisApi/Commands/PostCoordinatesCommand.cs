using CrisisApi.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrisisApi.Commands
{
    public class PostCoordinatesCommand : IRequest<Coordinates>
    {
        public Coordinates Coordinates { get; private set; }

        public PostCoordinatesCommand(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }
    }

    public class PostCoordinatesCommandHandler : IRequestHandler<PostCoordinatesCommand, Coordinates>
    {
        private CrisisDbContext _dbContext;

        public PostCoordinatesCommandHandler(CrisisDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Coordinates> Handle(PostCoordinatesCommand request, CancellationToken cancellationToken)
        {

            var user = await _dbContext.FindAsync<Appuser>(request.Coordinates.Id);

            if(user == null)
            {
                //create new user
                var appUser = new Appuser() { Id = request.Coordinates.Id };
                _dbContext.Add(appUser);
                await _dbContext.SaveChangesAsync();

            }
            var addedTime = UnixTimeStampToDateTime(request.Coordinates.TimeStamp);

            var coordInfo = new Coordinatesinfo()
            {
                Id = Guid.NewGuid(),
                Latitude = request.Coordinates.Latitude,
                Longitude = request.Coordinates.Longitude,
                Addedtime = addedTime,
                Appuserid = request.Coordinates.Id
            };

            _dbContext.Add(coordInfo);
            await _dbContext.SaveChangesAsync();

            return request.Coordinates;
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
