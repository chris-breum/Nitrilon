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



        [HttpGet("all")]
        public IEnumerable<Event> GetAll()
        {
           Repository r = new();
            var events = r.GetAllEvent();

            return events;
        }

        [HttpGet("id/{id}")]
        public IEnumerable<Event> Get(int id)
        {
            Repository r = new();
            var e = r.GetEvent(id);
            return e;
        }

        [HttpGet("date/{date}")]
        public IEnumerable<Event> GetEventDate(DateTime date)
        {
            Repository r = new();
            var events = r.GetEventByDate(date);
            return (IEnumerable<Event>)events;
        }

        [HttpPost]

        public IActionResult Add(Event newEvent)
        {
            try
            {
                Repository r = new ();
                int createdId = r.Save(newEvent);
                // Do that db stuff
                return Ok(createdId);
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
          
                // Do that db stuff
               
                Repository r = new();
                r.Delete(id);
                return Ok();
           
        }

        [HttpPut("{id}")]
        public IActionResult Put(Event updatedEvent)
        {
            try
            {
                Repository r = new();
                r.Update(updatedEvent);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }



    }


        
}

