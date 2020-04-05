using System;
using System.Collections.Generic;

namespace CrisisApi.Models
{
    public partial class Appuser
    {
        public Appuser()
        {
            Coordinatesinfo = new HashSet<Coordinatesinfo>();
        }

        public Guid Id { get; set; }

        public virtual ICollection<Coordinatesinfo> Coordinatesinfo { get; set; }
    }
}
