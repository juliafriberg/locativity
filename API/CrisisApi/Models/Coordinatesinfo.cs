using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CrisisApi.Models
{
    public partial class Coordinatesinfo
    {
        public Guid Id { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public Guid Appuserid { get; set; }
        public DateTime Addedtime { get; set; }
        [JsonIgnore]
        public virtual Appuser Appuser { get; set; }
    }
}
