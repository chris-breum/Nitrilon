using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class EventRating
    {
        private int id;
        private int eventId;
        private int ratingId;

        public EventRating(int id, int eventId, int ratingId)
        {
            Id = id;
            EventId = eventId;
            RatingId = ratingId;
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id cannot be less than 0");
                }
                id = value;
            }
        }

        public int EventId
        {
            get { return eventId; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("EventId cannot be less than 0");
                }
                eventId = value;
            }
        }

        public int RatingId
        {
            get { return ratingId; }
            set
            {
                if (value > 3 || value < 0)
                {
                    throw new ArgumentException("Rating cannot be more than 3 or less than 0");
                }
                ratingId = value;
            }
        }
    }
}

