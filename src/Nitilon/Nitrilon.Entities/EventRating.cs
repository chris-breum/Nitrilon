using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class EventRating
    {
        public int ratingId;
        public int Id { get; set; }
        public int EventId { get; set; }
        public int RatingId { 
            get
            {
                return ratingId;
            }

            set
            {
                if (ratingId > 3 || ratingId < 0) 
                {
                    throw new ArgumentException("Rating cannot be more than 3");
                }
            }
                }
    }
}
