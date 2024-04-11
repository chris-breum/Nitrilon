namespace Nitilon.Api
{
    public class Event
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        
        public string Name { get; set; } = string.Empty;
       public int attendees { get; set; } 

        public string Description { get; set; } = string.Empty;

    }
}
