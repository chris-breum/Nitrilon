using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nitrilon.Entities;
using System.Linq;
using System.Runtime.InteropServices;

namespace Nitilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        //List<Event> events = new List<Event>
        //   {
        //       new Event
        //       {
        //           Id = 0,
        //           Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        //           Name = "Birthday Party",
        //           attendees = 5,
        //           Description = "Birthday party for John"
        //       },
        //       new Event
        //       {
        //           Id = 1,
        //           Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
        //           Name = "Wedding",
        //           attendees = 9,
        //           Description = "Wedding of John and Jane"
        //       },
        //       new Event
        //       {
        //           Id = 2,
        //           Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
        //           Name = "Graduation",
        //           attendees = 1,
        //           Description = "Graduation of John"
        //       }
        //   };
        //[HttpPost]
        //public async Task<ActionResult<List<Event>>> GetAllEvent()
        //{
        //    var eventen = events;
        //    return Ok(eventen);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<List<Event>>> GetEvent(int id)
        //{
        //    var eventen = events.FirstOrDefault(x => x.Id == id);
        //    if (eventen == null)
        //    {
        //        return NotFound("event not found");
        //    }
        //    else 
        //    {
        //    return Ok(eventen);
        //    }

        //}
        //[HttpPost]
        //public async Task<ActionResult<List<Event>>> AddEvent(Event newEvent)
        //{


        //    events.Add(newEvent);

        //    var eventen = events;
        //    return Ok(eventen);

        //}

        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            List<Event> events = new List<Event>
            {
                new Event
                {
                    Id = 0,
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Name = "Birthday Party",
                    Attendees = 5,
                    Description = "Birthday party for John"
                },
                new Event
                {
                    Id = 1,
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                    Name = "Wedding",
                    Attendees = 9,
                    Description = "Wedding of John and Jane"
                },
                new Event
                {
                    Id = 2,
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
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
                // Do that db stuff
                return Ok();
            }
            catch (Exception ex)
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

