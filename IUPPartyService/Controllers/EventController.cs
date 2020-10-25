using System;
using System.Collections.Generic;
using System.Linq;
using IUPPartyService.Context;
using IUPPartyService.Models;
using IUPPartyService.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace IUPPartyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IUPPartyContext iUPPartyContext;
        public IConfiguration Configuration { get; }

        public EventController(IUPPartyContext iUPPartyContext, IConfiguration configuration)
        {
            this.iUPPartyContext = iUPPartyContext;
            Configuration = configuration;
        }

        [HttpPost("createEvent")]
        public IActionResult Add([FromBody] NewEventRequest newEventRequest)

        {
            if (newEventRequest.Name == null || newEventRequest.Description == null || newEventRequest.DateStart == null || newEventRequest.DateEnd == null || newEventRequest.Host == null || newEventRequest.HostName == null || newEventRequest.Image == null)
            {
                return BadRequest();
            }

            return CreateNewEvent(newEventRequest);
        }

        [HttpGet("getAllEvents")]
        public IActionResult GetEvents()
        {
            IList<FilteredEventAll> events = iUPPartyContext.Events.Select(e => new FilteredEventAll {
                EventID = e.EventID,
                Name = e.Name,
                HostName = e.HostName,
                Image = e.ImageData
            }).ToArray();

            return Ok(events);
        }

        [HttpGet("getNearestEvents")]
        public IActionResult GetNearestEvents([FromQuery(Name = "latitude")] string latitude, [FromQuery(Name = "longitude")] string longitude)
        {
            double lat = Convert.ToDouble(latitude);
            double lon = Convert.ToDouble(longitude);

            IList<FilteredEvent> events = iUPPartyContext.Events.Select(e => new FilteredEvent {
                EventID = e.EventID,
                Name = e.Name,
                HostName = e.HostName,
                Distance = CalculateDistance(lat, lon, e.Latitude, e.Longitude),
                Participants = e.Participant.ToArray().Length,
                Image = e.ImageData
            }).ToArray();

            IList<FilteredEvent> eventsFilt = events.Where(e => e.Distance <= 20000).OrderBy(e => e.Distance).ToArray();

            return Ok(eventsFilt);
        }

        [HttpPost("{eventID}/join")]
        public IActionResult Join([FromBody] JoinEventRequest joinEventRequest,[FromRoute] string eventID)
        {
            Event e = iUPPartyContext.Events.Find(eventID);
            Participant p = iUPPartyContext.Participant.Find(joinEventRequest.ParticipantKennitala);

            if (p == null)
            {
                if (e == null)
                {
                    return NotFound("Event with this ID does not exist.");
                }
                else
                {
                    if (e.Participant.ToArray().Length < e.MaxPeople)
                    {
                        if (e.Participant.FirstOrDefault(p => p.ParticipantKennitala == joinEventRequest.ParticipantKennitala) == null)
                        {
                            Participant part = new Participant
                            {
                                EventRef = e.EventID,
                                ParticipantKennitala = joinEventRequest.ParticipantKennitala,
                                ParticipantName = joinEventRequest.ParticipantName
                            };

                            iUPPartyContext.Participant.Add(part);
                            iUPPartyContext.SaveChanges();

                            return Ok();
                        }
                        else
                        {
                            return BadRequest("Cannot join twice to the same event.");
                        }

                    }
                    else
                    {
                        return BadRequest("This event has already reached the number of people allowed.");
                    }

                }
            }
            else
            {
                return Forbid("You cannot join more than one event.");
            }

            
        }

        public static double CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var d1 = latitude1 * (Math.PI / 180.0);
            var num1 = longitude1 * (Math.PI / 180.0);
            var d2 = latitude2 * (Math.PI / 180.0);
            var num2 = longitude2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return Math.Round(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))), 0);
        }

        private IActionResult CreateNewEvent(NewEventRequest newEventRequest)
        {
            try
            {
                Event e = new Event(
                    newEventRequest.Name,
                    newEventRequest.Description,
                    newEventRequest.MaxPeople,
                    DateTime.ParseExact(newEventRequest.DateStart, "dd/MM/yyyy", null),
                    DateTime.ParseExact(newEventRequest.DateEnd, "dd/MM/yyyy", null),
                    newEventRequest.Host,
                    newEventRequest.HostName,
                    newEventRequest.Latitude,
                    newEventRequest.Longitude,
                    Convert.FromBase64String(newEventRequest.Image)
                );

                iUPPartyContext.Events.Add(e);
                iUPPartyContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                //Eccezione da gestire nel modo corretto
                return StatusCode(500, e);
            }

        }

    }
}
