using System;

namespace CrisisApi.Models
{
    public class Coordinates
    {
        public Guid Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double TimeStamp { get; set; }
    }
}
