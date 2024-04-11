using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nitrilon.DataAccess;
using Nitrilon.Entities;
using System.Linq;
using System.Runtime.InteropServices;

namespace Nitilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

     

        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            List<Event> events = new List<Event>
            {
                new Event
                {
                    Id = 0,
                    Date = DateTime.Today,
                    Name = "Birthday Party",
                    Attendees = 5,
                    Description = "Birthday party for John"
                },
                new Event
                {
                    Id = 1,
                    Date = DateTime.Today,
                    Name = "Wedding",
                    Attendees = 9,
                    Description = "Wedding of John and Jane"
                },
                new Event
                {
                    Id = 2,
                    Date = DateTime.Today,
                    Name = "Graduation",
                    Attendees = 1,
                    Description = "Graduation of John"
                }
            };
            return events;
        }

        [HttpPost]

        public IActionResult Add(Event newEvent)
        {
            try
            {
                Repository r = new ();
                int createdId = r.Save(newEvent);
                // Do that db stuff
                return Ok();
            }
            catch (Exception e)
            {
               
                // return a 500 error
                //return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                return StatusCode(500);
            }
           
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Do that db stuff
                return Ok();
            }
            catch (Exception ex)
            {
                // return a 500 error
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Event updatedEvent)
        {
            try
            {
                // Do that db stuff
                return Ok();
            }
            catch (Exception ex)
            {
                // return a 500 error
                return StatusCode(500);
            }
        }



    }


        
}

