using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    internal class EventRating
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int Rating { get; set; }
    }
}
