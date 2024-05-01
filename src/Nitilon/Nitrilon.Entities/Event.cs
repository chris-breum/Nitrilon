namespace Nitrilon.Entities
{
    public class Event
    {
        #region Fields and constants
        public readonly DateTime EarliestPossibleEvent = new(2018, 1, 1);
        private int id;
        private string name;
        private DateTime date;
        private int attendees;
        private string description;
        private EventRatingData ratings;
        #endregion

        #region Constructors
        public Event(int id, string name, DateTime date, int attendees, string description)
        {
            Id = id;
            Name = name;
            Date = date;
            Attendees = attendees;
            Description = description;
        }

        public Event(int id, string name, DateTime date, int attendees, string description, EventRatingData ratings)
            : this(id, name, date, attendees, description)
        {
            Ratings = ratings;
        }
        #endregion

        #region Properties
        public int Id
        {
            get => id;
            private set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                if (id != value)
                {
                    id = value;
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNullOrWhiteSpace(value);
                if (name != value)
                {
                    name = value;
                }
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, EarliestPossibleEvent);
                if (date != value)
                {
                    date = value;
                }
            }
        }

        public int Attendees
        {
            get => attendees;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, -1);
                if (attendees != value)
                {
                    attendees = value;
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                }
            }
        }

        public EventRatingData Ratings { get => ratings; set => ratings = value; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds a rating the this event.
        /// </summary>
        /// <param name="rating">The rating to add to this event.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided rating is null.</exception>
        //public void Add(EventRatingData rating)
        //{
        //    if(rating == null)
        //    {
        //        throw new ArgumentNullException(nameof(rating));
        //    }
        //    ratings.Add(rating);
        //}

        /// <summary>
        /// Gets the average rating value of the ratings for this event.
        /// </summary>
        /// <returns>The average rating value. When there are no ratings for this event, the value -1.0 is returned.</returns>
        //public double GetRatingAverage()
        //{
        //    if(ratings.Count > 0)
        //    {
        //        double average = 0.0;
        //        int sum = 0;
        //        foreach(EventRatingData rating in ratings)
        //        {
        //            sum += rating.RatingValue;
        //        }
        //        average = (double)sum / (double)ratings.Count;
        //        return average;
        //    }
        //    else
        //    {
        //        return -1.0;
        //    }
        //}
        #endregion
    }
}