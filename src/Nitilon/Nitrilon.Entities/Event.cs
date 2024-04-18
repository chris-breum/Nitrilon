namespace Nitrilon.Entities
{
    public class Event
    {
        private int id;
        private DateTime date;
      private string name;
        private int attendees;
        private string description;
        private List<Rating> ratings;
      

        public Event(int id, DateTime date, string name, int attendees, string description, List<Rating> ratings )
        {
            Id = id;
            Date = date;
            Name = name;
            Attendees = attendees;
            Description = description;
            if(ratings == null)
            {
                throw new ArgumentNullException(nameof(ratings));
            }
            this.ratings = ratings;
                     
        }

        public int Id { get 
            {
                return id;
            } 
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id cannot be less than 0");
                }
                id = value;
            } 
        }
        public DateTime Date { get
            {
                return date;
            }
            set
            {
                if (value.Year < 2000)
                {
                    throw new ArgumentException("Date cannot be before year 2000");
                }
                date = value;
            }
        }

        public string Name { get 
            { 
                return name;
            }
            set 
            { 
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name cannot be null or empty");
                }
                name = value;
            }
        } 
        public int Attendees { get 
            {
                return attendees;
            }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("Attendees cannot be less than -1");
                }
                attendees = value;
            }
        }

        public string Description { get
            {
                return description;
            }
            set
            {
                description = value;
                
            }
        } 
       public void Add(Rating ratings)
        {
            if(ratings == null)
            {
                throw new ArgumentNullException(nameof(ratings));
            }
            this.ratings.Add(ratings);
        }

        /// <summary>
        /// Returns the average rating of the event
        /// </summary>
        /// <returns></returns>

        public double GetAverageRating()
        {
            if (ratings.Count > 0)
            {

                double sum = 0;
                foreach (Rating rating in ratings)
                {
                    sum += rating.Ratingvalue;
                }
                return (double)sum / (double)ratings.Count;
            }
            else
            {
                return 0;
            }
        }


    }
}
