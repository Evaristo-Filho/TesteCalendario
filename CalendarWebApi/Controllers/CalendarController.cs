using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarWebApi.DataAccess;
using CalendarWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CalendarWebApi.DTO;

namespace CalendarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private IRepository repository;

        public CalendarController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(repository.GetCalendar());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateForm form)
        {
            var calendarEvent = new Calendar() { EventOrganizer = form.EventOrganizer, Location = form.Location, Members = form.Members, Name = form.Name, Time = form.Time };
            var response = await repository.AddEvent(calendarEvent);
            return Created("",response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(CreateForm form, [FromQuery] int id)
        {
            var calendarEvent = new Calendar() { Id = id, EventOrganizer = form.EventOrganizer, Location = form.Location, Members = form.Members, Name = form.Name, Time = form.Time };
            await repository.UpdateEvent(calendarEvent);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var calendarEvent = new Calendar() { Id = id };
            await repository.DeleteEvent(null);
            return Ok();
        }

    }
}
