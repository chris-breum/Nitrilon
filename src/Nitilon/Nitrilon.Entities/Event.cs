namespace Nitrilon.Entities
{
    public class Event
    {
      private string name;
        private int attendees;
        public int Id { get; set; }
        public DateTime Date { get; set; }

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

        public string Description { get; set; } 

    }
}
