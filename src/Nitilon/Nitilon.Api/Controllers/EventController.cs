﻿using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Retrieves all events.
        /// </summary>
        /// <returns>An enumerable collection of events.</returns>
        [HttpGet("all")]
        public IEnumerable<Event> GetAll()
        {
            Repository r = new Repository();
            var events = r.GetAllEvents();

            return events;
        }

        /// <summary>
        /// Retrieves an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event.</param>
        /// <returns>An enumerable collection of events.</returns>
        [HttpGet("id/{id}")]
        public IEnumerable<Event> Get(int id)
        {
            Repository r = new Repository();
            var e = r.GetEvent(id);
            return e;
        }

        /// <summary>
        /// Retrieves events by date.
        /// </summary>
        /// <param name="date">The date of the events.</param>
        /// <returns>An enumerable collection of events.</returns>
        [HttpGet("date/{date}")]
        public IEnumerable<Event> GetEventDate(DateTime date)
        {
            Repository r = new Repository();
            var events = r.GetEventByDate(date);
            return events;
        }

        /// <summary>
        /// Adds a new event.
        /// </summary>
        /// <param name="newEvent">The event to add.</param>
        /// <returns>The ID of the created event.</returns>
        [HttpPost]
        public IActionResult Add(Event newEvent)
        {
            try
            {
                Repository r = new Repository();
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

        /// <summary>
        /// Deletes an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>An IActionResult indicating the result of the deletion.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Repository r = new Repository();
            r.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Updates an event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns>An IActionResult indicating the result of the update.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(Event updatedEvent)
        {
            try
            {
                Repository r = new Repository();
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

