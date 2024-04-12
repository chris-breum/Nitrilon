using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nitrilon.DataAccess;
using Nitrilon.Entities;

namespace Nitilon.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRatingController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<EventRating> GetAll()
        {
            Repository r = new();
            var events = r.GetAllEventRating();

            return events;
        }

        [HttpGet("{id}")]
        public IEnumerable<EventRating> Get(int id)
        {
            Repository r = new();
            var e = r.GetEventRating(id);
            return e;
        }

        [HttpPost]
        public IActionResult Add(EventRating newEventRating)
        {
            try
            {
                Repository r = new();
                int createdId = r.Save(newEventRating);
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
    }
}
