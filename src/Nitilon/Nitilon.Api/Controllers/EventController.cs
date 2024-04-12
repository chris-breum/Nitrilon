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
           Repository r = new();
            var events = r.GetAllEvent();

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

