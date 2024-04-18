namespace Nitrilon.Entities
{
    public class Event
    {
        private int id;
        private DateTime date;
      private string name;
        private int attendees;
        private string description;

      

        public Event(int id, DateTime date, string name, int attendees, string description )
        {
            Id = id;
            Date = date;
            Name = name;
            Attendees = attendees;
            Description = description;
           
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

    }
}
