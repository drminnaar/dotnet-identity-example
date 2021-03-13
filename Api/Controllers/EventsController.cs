using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Api.Models;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("events")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public sealed class EventsController : ControllerBase
    {
        [HttpGet(Name = nameof(GetEvents))]
        [ProducesResponseType(typeof(EventDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEvents()
        {
            var events = await Task.FromResult(GenerateEvents());

            return Ok(events);
        }

        [HttpOptions(Name = nameof(GetEventsOptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetEventsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

        private static IReadOnlyCollection<EventDto> GenerateEvents()
        {
            return CreateEventFaker().Generate(10);
        }

        private static Faker<EventDto> CreateEventFaker()
        {
            return new Faker<EventDto>()
                .StrictMode(true)
                .RuleFor(e => e.Date, faker => faker.Date.Future())
                .RuleFor(e => e.Description, faker => faker.Lorem.Sentence())
                .RuleFor(e => e.EventId, Guid.NewGuid())
                .RuleFor(e => e.HostedBy, faker => faker.Company.CompanyName())
                .RuleFor(e => e.Name, faker => faker.Lorem.Sentence())
                .RuleFor(e => e.Venue, faker => faker.Address.FullAddress());
        }
    }
}
