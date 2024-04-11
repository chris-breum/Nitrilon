﻿namespace Nitrilon.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string Name { get; set; } 
        public int Attendees { get; set; }

        public string Description { get; set; } 

    }
}
