using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApi.Models
{
    public class IdentifierObject
    {
        public Guid Id { get; set; }

        public IdentifierObject()
        {
            Id = Guid.NewGuid();
        }
    }
}
