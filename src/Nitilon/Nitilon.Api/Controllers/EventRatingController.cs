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
        /// <summary>
        /// Retrieves the event rating data for a specific event.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <returns>The event rating data.</returns>
        [HttpGet]
        public ActionResult<EventRatingData> GetEventRatingDataFor(int eventId)
        {
            EventRepository repository = new();
            EventRatingData eventRatingData = repository.GetEventRatingDataBy(eventId);
            return Ok(eventRatingData);
        }

        /// <summary>
        /// Adds a new event rating.
        /// </summary>
        /// <param name="newEventRating">The new event rating to add.</param>
        /// <returns>The ID of the created event rating.</returns>
        [HttpPost]
        public IActionResult Add(EventRating newEventRating)
        {
            try
            {
                EventRepository r = new();
                int createdId = r.Save(newEventRating);
                // Do that db stuff
                return Ok(createdId);
            }
            catch (Exception )
            {
                // return a 500 error
                //return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                return StatusCode(500);
            }
        }

    }
}
